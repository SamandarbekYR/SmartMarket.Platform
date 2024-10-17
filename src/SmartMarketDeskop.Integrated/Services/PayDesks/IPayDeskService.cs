using SmartMarket.Service.DTOs.PayDesks;

namespace SmartMarketDeskop.Integrated.Services.PayDesks;

public interface IPayDeskService
{
    Task<List<PayDesksDto>> GetAll();
    Task<bool> CreatePayDesk(AddPayDesksDto dto);
    Task<bool> UpdatePayDesk(AddPayDesksDto dto, Guid id);
    Task<bool> DeletePayDesk(Guid id);
}
