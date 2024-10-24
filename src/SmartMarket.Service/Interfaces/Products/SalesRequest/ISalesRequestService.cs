using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarket.Service.Interfaces.Products.SalesRequest
{
    public interface ISalesRequestService
    {
        Task<bool> AddAsync(AddSalesRequestDto dto);
        Task<bool> UpdateAsync(AddSalesRequestDto dto, Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        Task<SalesRequestDto> GetByIdAsync(Guid Id);
        Task<List<SalesRequestDto>> GetAllAsync();
        Task<List<SalesRequestDto>> FilterProductSaleAsync(FilterSalesRequestDto dto);
    }
}
