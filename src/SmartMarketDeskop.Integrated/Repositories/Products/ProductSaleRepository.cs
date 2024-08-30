using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class ProductSaleRepository : Repository<ProductSaleView>, IProductSale
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ProductSaleView> _productSales;

        public ProductSaleRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _productSales = appDb.Set<ProductSaleView>();
        }

        public async Task<List<ProductSaleView>> GetProductSalesFullInformationAsync()
        {
            return await _productSales
                .Include(ps => ps.ProductView)
                .Include(ps => ps.WorkerView)
                .Include(ps => ps.TransactionView)
                .Include(ps => ps.PayDeskView)
                .Include(ps => ps.ReplaceProductViews)
                .Include(ps => ps.InValidProductViews)
                .ToListAsync();
        }
    }
}