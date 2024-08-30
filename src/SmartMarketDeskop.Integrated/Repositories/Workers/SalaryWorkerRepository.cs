using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers
{
    public class SalaryWorkerRepository : Repository<SalaryWorkerView>, ISalaryWorker
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<SalaryWorkerView> _salaryWorkers;

        public SalaryWorkerRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _salaryWorkers = appDb.Set<SalaryWorkerView>();
        }

        public async Task<List<SalaryWorkerView>> GetSalaryWorkersFullInformationAsync()
        {
            return await _salaryWorkers
                .Include(sw => sw.WorkerView)
                .Include(sw => sw.SalaryView)
                .ToListAsync();
        }
    }
}