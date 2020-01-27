using HostedService.Data.Models;
using Lamar;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HostedService
{
    public abstract class HandlerBase
    {
        public IServiceProvider _services { get; set; }
        public IContainer _container { get; set; }
        public ContosoContext _db { get; set; }
        public IMediator _mediator { get; set; }
    }
}
