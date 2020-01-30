using HostedService.Data.Models;
using HostedService.Services;
using StructureMap;

namespace HostedService
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            For<Service1>().Use<Service1>().ContainerScoped();
            For<Service2>().Use<Service2>().ContainerScoped();
            For<ContosoContext>().Use<ContosoContext>().ContainerScoped();
        }
    }
}
