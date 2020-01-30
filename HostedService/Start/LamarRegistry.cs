using HostedService.Data.Models;
using HostedService.Services;
using Lamar;

namespace HostedService
{
    public class LamarRegistry : ServiceRegistry
    {
        public LamarRegistry()
        {
            For<Service1>().Use<Service1>().Scoped();
            For<Service2>().Use<Service2>().Scoped();
            For<ContosoContext>().Use<ContosoContext>().Scoped();
        }
    }
}
