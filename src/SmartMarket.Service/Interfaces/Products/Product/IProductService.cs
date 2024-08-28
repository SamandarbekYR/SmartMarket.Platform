using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Interfaces.Products.Product;

public interface IProductService
{
    Task<bool> AddAsync(AddProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ProductDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddProductDto dto, Guid Id);
}
