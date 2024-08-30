using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class ReplaceProductRepository : Repository<ReplaceProductView>, IReplaceProduct
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ReplaceProductView> _replaceProducts;

        public ReplaceProductRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _replaceProducts = appDb.Set<ReplaceProductView>();
        }

        public async Task<List<ReplaceProductView>> GetReplaceProductsFullInformationAsync()
        {
            return await _replaceProducts
                .Include(rp => rp.ProductSaleView)
                .Include(rp => rp.WorkerView)
                .ToListAsync();
        }
    }
}