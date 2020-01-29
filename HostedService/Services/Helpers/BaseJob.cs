using HostedService.Data.Models;

namespace HostedService.Services
{
    public abstract class BaseJob
    {
        public ContosoContext Context { get; }

        public BaseJob(ContosoContext context)
        {
            Context = context;
        }
    }
}
