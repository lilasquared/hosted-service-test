using HostedService.Data.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HostedService.Services
{
    public abstract class BaseJob
    {
        //public ContosoContext _db { get; set; }
        public IMediator _mediator { get; set; }
    }
}
