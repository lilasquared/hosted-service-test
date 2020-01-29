using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace HostedService.Services
{
    public class Service1 : BaseJob, IAsyncJob
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var items = await Context.Courses.ToListAsync(cancellationToken);

            Console.WriteLine($"Hello from {GetType().Name}, ItemCount: {items.Count()}");

            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
        }

        public Service1(ContosoContext context) : base(context)
        {
        }
    }
}
