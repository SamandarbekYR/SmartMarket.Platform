using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products;

public class SalesRequestServer : ISalesRequestsServer
{
    public Task<bool> AddAsync(AddSalesRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<IList<SalesRequestDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SalesRequestDto> GetByIdAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(AddSalesRequestDto dto, Guid Id)
    {
        throw new NotImplementedException();
    }
}
