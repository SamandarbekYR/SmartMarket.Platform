using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class SalaryWorkerRepository : Repository<SalaryWorker>, ISalaryWorker
    {
        public SalaryWorkerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
