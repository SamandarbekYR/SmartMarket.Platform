using SmartMarket.Service.DTOs.Products.InvalidProduct;

namespace SmartMarket.Service.Interfaces.Products.InvalidProduct;

public interface IInvalidProductService
{
    Task<bool> AddAsync(AddInvalidProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id);
    Task<List<InvalidProductDto>> GetAllAsync();
    Task<List<InvalidProductDto>> FilterInvalidProductAsync(FilterInvalidProductDto filterInvalidProductDto);
    Task<List<InvalidProductDto>> GetInvalidProductsByProductNameAsync(string productName);
}
