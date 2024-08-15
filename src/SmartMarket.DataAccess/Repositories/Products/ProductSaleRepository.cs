using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class ProductSaleRepository : Repository<ProductSale>, IProductSale
    {
        public ProductSaleRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
