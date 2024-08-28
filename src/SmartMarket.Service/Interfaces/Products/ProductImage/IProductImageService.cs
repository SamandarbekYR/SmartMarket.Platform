using SmartMarket.Service.DTOs.Products.ProductImage;

namespace SmartMarket.Service.Interfaces.Products.ProductImage;

public interface IProductImageService
{
    Task<bool> AddAsync(AddProductImageDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ProductImageDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddProductImageDto dto, Guid Id);
}
