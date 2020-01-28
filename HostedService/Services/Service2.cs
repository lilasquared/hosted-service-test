using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Handlers;
using MediatR;

namespace HostedService.Services
{
    public class Service2 : BaseJob, IAsyncJob
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var items = await Mediator.Send(new GetStudentGrades(), cancellationToken);

            Console.WriteLine($"Hello from {GetType().Name}, ItemCount: {items.Count()}");

            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
        }

        public Service2(IMediator mediator) : base(mediator)
        {
        }
    }
}
