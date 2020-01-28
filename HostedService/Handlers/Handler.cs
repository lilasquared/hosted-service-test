using System;
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
                return await Db.Courses.ToListAsync(cancellationToken);
            }

            public GetByItemsHandler(ContosoContext db) : base(db)
            {
            }
        }

        public class GetStudentGradesHandler : HandlerBase, IRequestHandler<GetStudentGrades, IEnumerable<StudentGrade>>
        {
            public async Task<IEnumerable<StudentGrade>> Handle(GetStudentGrades message, CancellationToken cancellationToken)
            {
                return await Db.StudentGrades.ToListAsync(cancellationToken);
            }

            public GetStudentGradesHandler(ContosoContext db) : base(db)
            {
            }
        }
    }

    public class GetCourses : IRequest<IEnumerable<Course>> { }

    public class GetStudentGrades : IRequest<IEnumerable<StudentGrade>> { }
}
