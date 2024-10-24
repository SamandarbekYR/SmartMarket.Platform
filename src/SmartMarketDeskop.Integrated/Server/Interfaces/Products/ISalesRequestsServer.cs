using SmartMarket.Service.DTOs.Products.SalesRequest;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface ISalesRequestsServer
{
    Task<bool> AddAsync(AddSalesRequestDto dto);
    Task<bool> UpdateAsync(AddSalesRequestDto dto, Guid Id);
    Task<SalesRequestDto> GetByIdAsync(Guid Id);
    Task<IList<SalesRequestDto>> GetAllAsync(); 
    Task<IList<SalesRequestDto>> FilterSalesRequestAsync(FilterSalesRequestDto dto);
}
