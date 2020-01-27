using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostedService
{
    interface IAsyncJob
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}
