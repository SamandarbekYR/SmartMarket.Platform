using SmartMarket.Service.DTOs.Products.InvalidProduct;

namespace SmartMarket.Service.Interfaces.Products.InvalidProduct;

public interface IInvalidProductService
{
    Task<bool> AddAsync(AddInvalidProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<InvalidProductDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id);
}
