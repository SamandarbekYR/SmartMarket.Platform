using Et = SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents;

public interface IContrAgentService
{
    Task<List<ContrAgentViewModels>> GetAll();
    Task<bool> AddAsync(ContrAgentDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id);
    Task<List<ContrAgentViewModels>> GetByName(string name);
    Task<Et.ContrAgentDto> GetById(Guid id);
}
