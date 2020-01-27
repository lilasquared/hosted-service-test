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
                _.WithDefaultConventions();
                _.TheCallingAssembly();
                _.AssemblyContainingType<ContosoContext>();

                // MediatR
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<,>));
                _.ConnectImplementationsToTypesClosing(typeof(IRequestHandler<>));
                _.ConnectImplementationsToTypesClosing(typeof(INotificationHandler<>));
            });

            For<ContosoContext>().Use<ContosoContext>().Transient();

            // MediatR
            For<IMediator>().Use<Mediator>().Transient();

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
