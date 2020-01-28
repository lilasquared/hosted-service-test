using HostedService.Data.Models;
using Lamar;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

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

                // MediatR
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });

            For<IHostedService>().Use<ServiceWorker>().Singleton();
            For<ContosoContext>().Use<ContosoContext>().Scoped();

            // MediatR
            For<ServiceFactory>().Use(x => x.GetInstance);
            For<IMediator>().Use<Mediator>().Scoped();

            Policies.SetAllProperties(y => y.OfType<ContosoContext>());
            Policies.SetAllProperties(y => y.OfType<IHostEnvironment>());
            Policies.SetAllProperties(y => y.OfType<IConfiguration>());
            Policies.SetAllProperties(y => y.OfType<IContainer>());
            Policies.SetAllProperties(y => y.OfType<IServiceProvider>());

            // MediatR
            Policies.SetAllProperties(y => y.OfType<IMediator>());
        }
    }
}
