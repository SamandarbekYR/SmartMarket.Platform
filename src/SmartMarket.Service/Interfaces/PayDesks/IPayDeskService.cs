using SmartMarket.Service.DTOs.PayDesks;

namespace SmartMarket.Service.Interfaces.PayDesks;

public interface IPayDeskService
{
    Task<bool> AddAsync(AddPayDesksDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PayDesksDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddPayDesksDto dto, Guid Id);
}
