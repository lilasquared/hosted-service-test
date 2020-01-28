using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    interface IAsyncJob
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
