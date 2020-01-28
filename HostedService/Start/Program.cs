using HostedService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using HostedService.Services;
using MediatR;

namespace HostedService
{
    public class Program
    {
        public static async Task Main(String[] args)
        {
            try
            {
                var host = new HostBuilder()
                    .ConfigureHostConfiguration(configHost =>
                    {
                        configHost.SetBasePath(Directory.GetCurrentDirectory());
                        configHost.AddJsonFile("hostsettings.json", optional: true);
                        configHost.AddEnvironmentVariables();
                        configHost.AddCommandLine(args);
                    })
                    .ConfigureAppConfiguration((hostContext, configApp) =>
                    {
                        configApp.AddEnvironmentVariables();
                        configApp.AddCommandLine(args);
                        configApp.AddUserSecrets<Program>(false);
                    })
                    .ConfigureServices((hostContext, services) =>
                    {
                        services.AddDbContext<ContosoContext>(ctx => ctx.UseInMemoryDatabase("Contoso"));
                    })
                    //.UseLamar(new LamarRegistry())
                    .UseMicrosoft()
                    .UseConsoleLifetime()
                    .Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL - Task Main():{ex}");
            }
        }
    }

    public static class ServiceCollectionExtensions
    {
        public static IHostBuilder UseMicrosoft(this IHostBuilder builder)
        {
            return builder.ConfigureServices((hostContext, services) =>
            {
                services.AddMediatR(Assembly.GetExecutingAssembly());

                services.AddScoped<Service1>();
                services.AddScoped<Service2>();
                services.AddHostedService<ServiceWorker>();
            });
        }
    }
}
