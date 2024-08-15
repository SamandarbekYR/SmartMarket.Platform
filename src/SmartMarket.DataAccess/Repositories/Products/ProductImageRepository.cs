using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImage
    {
        public ProductImageRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
