using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class WorkerRepository : Repository<Worker>, IWorker
    {
        public WorkerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
