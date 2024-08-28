using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IProductSale : IRepository<ProductSale>
    {
        public Task<List<ProductSale>> GetProductSalesFullInformationAsync();
    }
}
