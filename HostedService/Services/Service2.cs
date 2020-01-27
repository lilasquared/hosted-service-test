using HostedService.Data.Models;
using HostedService.Handlers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.Services
{
    public class Service2 : BaseJob, IAsyncJob
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var items = await _mediator.Send(new GetStudentGrades());

                Console.WriteLine($"Hello from {GetType().Name}, ItemCount: {items.Count()}");

                await Task.Delay(TimeSpan.FromMilliseconds(2));
            }
        }
    }
}
