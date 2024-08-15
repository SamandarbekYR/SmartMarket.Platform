using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class InvalidProductRepository : Repository<InvalidProduct>, IInvalidProduct
    {
        public InvalidProductRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
