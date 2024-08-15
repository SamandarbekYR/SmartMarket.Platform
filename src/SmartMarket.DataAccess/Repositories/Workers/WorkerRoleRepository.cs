using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class WorkerRoleRepository : Repository<WorkerRole>, IWorkerRole
    {
        public WorkerRoleRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
