using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class WorkerDebtRepository : Repository<WorkerDebt>, IWorkerDebt
    {
        public WorkerDebtRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
