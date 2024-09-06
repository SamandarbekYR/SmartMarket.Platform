using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgents
{
    public interface IContrAgentService
    {
        Task<List<ContrAgentViewModels>> GetAll();
        Task<bool> AddAsync(ContrAgentDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id);
    }
}
