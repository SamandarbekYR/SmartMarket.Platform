using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IReplaceProduct : IRepository<ReplaceProduct>
    {
        public Task<List<ReplaceProduct>> GetReplaceProductsFullInformationAsync();
    }
}
