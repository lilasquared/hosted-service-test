using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace HostedService
{
    public class StructureMapContainerFactory : IServiceProviderFactory<IContainer>
    {
        private readonly IContainer _container;

        // pass any dependencies to your factory
        public StructureMapContainerFactory(IContainer container)
        {
            _container = container;
        }

        public IContainer CreateBuilder(IServiceCollection services)
        {
            _container.Populate(services);
            return _container;
        }

        public IServiceProvider CreateServiceProvider(IContainer container)
        {
            return container.GetInstance<IServiceProvider>();
        }
    }
}
