using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers
{
    public class WorkerDebtRepository : Repository<WorkerDebtView>, IWorkerDebt
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<WorkerDebtView> _workerDebts;

        public WorkerDebtRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _workerDebts = appDb.Set<WorkerDebtView>();
        }

        public async Task<List<WorkerDebtView>> GetWorkerDebtsFullInformationAsync()
        {
            return await _workerDebts
                .Include(wd => wd.WorkerView)
                .ToListAsync();
        }
    }
}
