using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers
{
    public class SalaryCheckRepository : Repository<SalaryCheckView>, ISalaryCheck
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<SalaryCheckView> _salaryChecks;

        public SalaryCheckRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _salaryChecks = appDb.Set<SalaryCheckView>();
        }

        public async Task<List<SalaryCheckView>> GetSalaryChecksFullInformationAsync()
        {
            return await _salaryChecks
                .Include(sc => sc.WorkerView)
                .ToListAsync();
        }
    }
}
