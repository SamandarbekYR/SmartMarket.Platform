using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class InvalidProductRepository : Repository<InValidProductView>, IInvalidProduct
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<InValidProductView> _invalidProducts;

        public InvalidProductRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _invalidProducts = appDb.Set<InValidProductView>();
        }

        public async Task<List<InValidProductView>> GetInvalidProductsFullInformationAsync()
        {
            return await _invalidProducts
                .Include(ip => ip.WorkerView)
                .Include(ip => ip.ProductSaleView)
                .ToListAsync();
        }
    }
}