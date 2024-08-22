using SmartMarket.Service.DTOs.ReplaceProduct;

namespace SmartMarket.Service.Interfaces.ReplaceProduct;

public interface IReplaceProductService
{
    Task<bool> AddAsync(AddReplaceProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ReplaceProductDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id);
}
