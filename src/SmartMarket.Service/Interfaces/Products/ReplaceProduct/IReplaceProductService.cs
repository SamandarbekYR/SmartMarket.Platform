using SmartMarket.Service.DTOs.Products.ReplaceProduct;

namespace SmartMarket.Service.Interfaces.Products.ReplaceProduct;

public interface IReplaceProductService
{
    Task<bool> AddAsync(AddReplaceProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id);
    Task<List<ReplaceProductDto>> GetAllAsync();
    Task<List<ReplaceProductDto>> FilterReplaceProductAsync(FilterReplaceProductDto dto);
}
