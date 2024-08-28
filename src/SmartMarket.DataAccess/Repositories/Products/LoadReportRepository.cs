using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class LoadReportRepository : Repository<LoadReport>, ILoadReport
{
    private readonly AppDbContext _appDbContext;
    private DbSet<LoadReport> _loadReports;

    public LoadReportRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _loadReports = appDb.Set<LoadReport>();
    }

    public async Task<List<LoadReport>> GetLoadReportsFullInformationAsync()
    {
        return await _loadReports
            .Include(lr => lr.Worker)      
            .Include(lr => lr.Product)     
            .Include(lr => lr.ContrAgent)  
            .ToListAsync();
    }
}
