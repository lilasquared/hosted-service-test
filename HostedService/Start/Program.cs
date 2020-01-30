using HostedService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;
using HostedService.Services;
using Lamar.Microsoft.DependencyInjection;
using StructureMap;

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
                        services.AddHostedService<ServiceWorker>();
                    })
                    .UseLamar(new LamarRegistry())
                    //.UseMicrosoft()
                    //.UseStructureMap()
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
                services.AddScoped<Service1>();
                services.AddScoped<Service2>();
            });
        }

        public static IHostBuilder UseStructureMap(this IHostBuilder builder)
        {
            return builder.UseServiceProviderFactory(new StructureMapContainerFactory(new Container(new StructureMapRegistry())));
        }
    }
}
