using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IInvalidProduct : IRepository<InvalidProduct>
    {
        public Task<List<InvalidProduct>> GetInvalidProductsFullInformationAsync();
    }
}
