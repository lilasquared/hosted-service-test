using HostedService.Data.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService.Handlers
{
    public class Handler
    {
        public class GetByItemsHandler : HandlerBase, IRequestHandler<GetCourses, IEnumerable<Course>>
        {
            public async Task<IEnumerable<Course>> Handle(GetCourses message, CancellationToken cancellationToken)
            {
                return await _db.Courses.ToListAsync();
            }
        }

        public class GetStudentGradesHandler : HandlerBase, IRequestHandler<GetStudentGrades, IEnumerable<StudentGrade>>
        {
            public async Task<IEnumerable<StudentGrade>> Handle(GetStudentGrades message, CancellationToken cancellationToken)
            {
                //return await _db.StudentGrades.ToListAsync();

                using (var scope = _services.CreateScope())
                {
                    var db = scope.ServiceProvider.GetRequiredService<ContosoContext>();

                    return await db.StudentGrades.ToListAsync();
                }
            }
        }
    }

    public class GetCourses : IRequest<IEnumerable<Course>>
    {
        public GetCourses()
        {
        }
    }

    public class GetStudentGrades : IRequest<IEnumerable<StudentGrade>>
    {
        public GetStudentGrades()
        {
        }
    }
}
