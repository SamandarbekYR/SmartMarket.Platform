using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class ReplaceProductRepository : Repository<ReplaceProduct>, IReplaceProduct
    {
        public ReplaceProductRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
