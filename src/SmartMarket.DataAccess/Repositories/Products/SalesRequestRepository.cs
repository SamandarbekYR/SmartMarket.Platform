using Microsoft.EntityFrameworkCore;

using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class SalesRequestRepository : Repository<SalesRequest>, ISalesRequest
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<SalesRequest> _salesRequests;

        public SalesRequestRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _salesRequests = appDb.Set<SalesRequest>();
        }
        public async Task<List<SalesRequest>> GetSalesRequestsFullInformationAsync()
        {
            return await _salesRequests
                .Include(sr => sr.PayDesk)
                .Include(sr => sr.Worker)
                .Include(sr => sr.ProductSaleItems)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
        }
    }
}
