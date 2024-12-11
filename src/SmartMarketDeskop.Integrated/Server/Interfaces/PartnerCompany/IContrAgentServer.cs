using CT = SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IContrAgentServer
{
    Task<List<ContrAgent>> GetAllAsync();
    Task<bool> AddAsync(ContrAgentDto dto);

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id);
    Task<List<CT.ContrAgentDto>> GetContrAgentByNameAsync(string name);
}
