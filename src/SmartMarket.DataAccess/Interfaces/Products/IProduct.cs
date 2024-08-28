using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IProduct : IRepository<Product>
    {
        public Task<List<Product>> GetProductsFullInformationAsync();
    }
}
