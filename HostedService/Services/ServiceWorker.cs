using HostedService.Services;
using Lamar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    public class ServiceWorker : IHostedService
    {
        public IContainer _container { get; set; }

        public ServiceWorker(IContainer container)
        {
            _container = container;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var svc1 = _container.GetInstance<Service1>();
            var svc2 = _container.GetInstance<Service2>();

            Task.Run(async () => await svc1.ExecuteAsync(cancellationToken));
            Task.Run(async () => await svc2.ExecuteAsync(cancellationToken));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => Console.WriteLine("Stopped"));

            return Task.CompletedTask;
        }
    }
}
