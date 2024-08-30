using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers
{
    public class WorkerRepository : Repository<WorkerView>, IWorker
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<WorkerView> _workers;

        public WorkerRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _workers = appDb.Set<WorkerView>();
        }

        public async Task<WorkerView?> GetPhoneNumberAsync(string phoneNumber)
        {
            return await _workers
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
        }

        public async Task<List<WorkerView>> GetWorkersFullInformationAsync()
        {
            return await _workers
                .Include(w => w.PositionView)
                .Include(w => w.WorkerRoleView)
                .Include(w => w.SalaryWorkerViews)
                    .ThenInclude(sw => sw.SalaryView)
                .Include(w => w.SalaryCheckViews)
                .Include(w => w.WorkerDebtViews)
                .Include(w => w.ProductViews)
                .Include(w => w.ProductSaleViews)
                .Include(w => w.LoadReportViews)
                .Include(w => w.ReplaceProductViews)
                .Include(w => w.InValidProductViews)
                .Include(w => w.OrderViews)
                .Include(w => w.ExpenseViews)
                .ToListAsync();
        }
    }
}