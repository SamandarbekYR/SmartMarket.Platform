using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products
{
    public interface IInvalidProductServer
    {
        Task<bool> AddAsync(AddReplaceProductDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id);
        Task<List<InvalidProductDto>> GetAllAsync();
        Task<List<InvalidProductDto>> FilterInvalidProductAsync(FilterInvalidProductDto filterInvalidProductDto);
    }
}
