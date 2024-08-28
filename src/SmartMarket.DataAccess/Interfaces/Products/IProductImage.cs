using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IProductImage : IRepository<ProductImage>
    {
        public Task<List<ProductImage>> GetProductImagesFullInformationAsync();
    }
}
