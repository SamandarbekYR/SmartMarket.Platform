using CT = SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IContrAgentServer
{
    Task<List<ContrAgentViewModels>> GetAllAsync();
    Task<bool> AddAsync(ContrAgentDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id);
    Task<CT.ContrAgentDto> GetByIdAsync(Guid id);
    Task<List<ContrAgentViewModels>> GetContrAgentByNameAsync(string name);
}
