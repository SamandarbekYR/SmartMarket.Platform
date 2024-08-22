using SmartMarket.Service.DTOs.ContrAgentPayment;

namespace SmartMarket.Service.Interfaces.ContrAgentPayment;

public interface IContrAgentPaymentService
{
    Task<bool> AddAsync(AddContrAgentPaymentDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ContrAgentPaymentDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddContrAgentPaymentDto dto, Guid Id);
}
