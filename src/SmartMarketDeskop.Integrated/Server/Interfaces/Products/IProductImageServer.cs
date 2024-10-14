using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Products.ProductImage;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductImageServer
{
    Task<List<ProductImage>> GetAllAsync();
    Task<bool> AddAsync(ProductImageDto dto);

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(ProductImageDto dto, Guid Id);
}
