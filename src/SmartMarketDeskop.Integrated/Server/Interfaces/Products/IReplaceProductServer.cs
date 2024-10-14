using SmartMarket.Service.DTOs.Products.ReplaceProduct;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products
{
    public interface IReplaceProductServer
    {
        Task<bool> AddAsync(AddReplaceProductDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id);
        Task<List<ReplaceProductDto>> GetAllAsync();
        Task<List<ReplaceProductDto>> FilterReplaceProductAsync(FilterReplaceProductDto dto);
    }
}
