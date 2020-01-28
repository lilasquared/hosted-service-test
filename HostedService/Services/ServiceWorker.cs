using HostedService.Services;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HostedService.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace HostedService
{
    public class ServiceWorker : IHostedService
    {
        private readonly ContosoContext _context;
        private readonly IServiceProvider _container;

        public ServiceWorker(IServiceProvider container, ContosoContext context)
        {
            _context = context;
            _container = container;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _context.Courses.AddRange(Enumerable.Range(1, 1000).Select(x => new Course
            {
                CourseId = x
            }));

            _context.StudentGrades.AddRange(Enumerable.Range(1, 1000).Select(x => new StudentGrade
            {
               EnrollmentId = x
            }));

            _context.SaveChanges();

            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    using var scope = _container.CreateScope();
                    await scope.ServiceProvider.GetService<Service1>().ExecuteAsync(cancellationToken).ConfigureAwait(false);
                }
            }, cancellationToken);
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    using var scope = _container.CreateScope();
                    await scope.ServiceProvider.GetService<Service2>().ExecuteAsync(cancellationToken).ConfigureAwait(false);
                }
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => Console.WriteLine("Stopped"), cancellationToken);

            return Task.CompletedTask;
        }
    }
}
