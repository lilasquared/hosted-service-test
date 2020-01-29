using HostedService.Data.Models;
using Lamar;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HostedService
{
    public class LamarRegistry : ServiceRegistry
    {
        public LamarRegistry()
        {
            Scan(_ =>
            {
                _.WithDefaultConventions(ServiceLifetime.Scoped);
                _.TheCallingAssembly();
                _.AssemblyContainingType<ContosoContext>();
            });

            For<IHostedService>().Use<ServiceWorker>().Singleton();
            For<ContosoContext>().Use<ContosoContext>().Scoped();
        }
    }
}
