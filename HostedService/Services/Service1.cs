using HostedService.Handlers;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace HostedService.Services
{
    public class Service1 : BaseJob, IAsyncJob
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var items = await Mediator.Send(new GetCourses(), cancellationToken);

            Console.WriteLine($"Hello from {GetType().Name}, ItemCount: {items.Count()}");

            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
        }

        public Service1(IMediator mediator) : base(mediator)
        {
        }
    }
}
