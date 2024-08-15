using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class LoadReportRepository : Repository<LoadReport>, ILoadReport
    {
        public LoadReportRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
