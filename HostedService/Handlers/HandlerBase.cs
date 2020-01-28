using HostedService.Data.Models;

namespace HostedService
{
    public abstract class HandlerBase
    {
        public ContosoContext Db { get; }

        public HandlerBase(ContosoContext db)
        {
            Db = db;
        }
    }
}
