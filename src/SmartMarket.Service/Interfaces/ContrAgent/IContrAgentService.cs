using SmartMarket.Service.DTOs.ContrAgent;

namespace SmartMarket.Service.Interfaces.ContrAgent;

public interface IContrAgentService
{
    Task<bool> AddAsync(AddContrAgentDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ContrAgentDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddContrAgentDto dto, Guid Id);
}
