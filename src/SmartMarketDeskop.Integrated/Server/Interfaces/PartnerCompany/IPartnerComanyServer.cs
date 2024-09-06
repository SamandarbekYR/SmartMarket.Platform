using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany
{
    public interface IPartnerComanyServer
    {
        Task<bool> AddCompany(PartnerCompanyDto companyDto);
        Task<List<PartnerCompanyView>> GetAllCompany();
    }
}
