using MediatR;

namespace HostedService.Services
{
    public abstract class BaseJob
    {
        public IMediator Mediator { get; }

        public BaseJob(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
