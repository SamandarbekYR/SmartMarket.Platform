using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class LoadReportRepository : Repository<LoadReportView>, ILoadReport
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<LoadReportView> _loadReports;

        public LoadReportRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _loadReports = appDb.Set<LoadReportView>();
        }

        public async Task<List<LoadReportView>> GetLoadReportsFullInformationAsync()
        {
            return await _loadReports
                .Include(lr => lr.WorkerView)
                .Include(lr => lr.ProductView)
                .Include(lr => lr.ContrAgentView)
                .ToListAsync();
        }
    }
}