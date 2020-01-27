using HostedService.Data.Models;
using Lamar.Microsoft.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace HostedService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var host = new HostBuilder()
                    .UseLamar<LamarRegistry>()
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
                        var conn = hostContext.Configuration["DB_CONN"];
                        var dbPassword = hostContext.Configuration["DB_PASSWORD"];

                        var builder = new SqlConnectionStringBuilder(conn)
                        {
                            Password = dbPassword
                        };

                        services.AddDbContext<ContosoContext>(ctx => ctx.UseSqlServer(builder.ConnectionString), ServiceLifetime.Transient);

                        // MediatR
                        services.AddMediatR(cfg => cfg.AsTransient(), Assembly.GetExecutingAssembly());
                        services.AddHostedService<ServiceWorker>();
                    })
                    .UseConsoleLifetime()
                    .Build();

                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CRITICAL - Task Main():{ex.ToString()}");
            }
        }
    }
}
