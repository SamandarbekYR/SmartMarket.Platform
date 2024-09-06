using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany
{
    public interface IContrAgentServer
    {
        Task<List<ContrAgent>> GetAllAsync();
        Task<bool> AddAsync(ContrAgentDto dto);

        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id);
    }
}
