using SmartMarket.Service.DTOs.PayDesks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PayDesks;

public interface IPayDesksServer
{
    Task<List<PayDesksDto>> GetAllAsync();
    Task<bool> AddAsync(AddPayDesksDto dto);
    Task<bool> UpdateAsync(AddPayDesksDto dto, Guid id);
    Task<bool> DeleteAsync(Guid id);
}
