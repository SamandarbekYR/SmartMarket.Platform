using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class ProductRepository : Repository<Product>, IProduct
    {
        public ProductRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
