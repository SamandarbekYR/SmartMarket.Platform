using SmartMarket.Service.DTOs.Products.SalesRequest;

namespace SmartMarketDeskop.Integrated.Services.Products.SalesRequests;

public interface ISalesRequestsService
{
    Task<(long, bool)> CreateSalesRequest(AddSalesRequestDto dto);
    Task<bool> UpdateSalesRequest(AddSalesRequestDto dto, Guid id);
    Task<SalesRequestDto> GetById(Guid id);
    Task<IList<SalesRequestDto>> GetAll();
    Task<IList<SalesRequestDto>> FilterSalesRequest(FilterSalesRequestDto dto);
}
