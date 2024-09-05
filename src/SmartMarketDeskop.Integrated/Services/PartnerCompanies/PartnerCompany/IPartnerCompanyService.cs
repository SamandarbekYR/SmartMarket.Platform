using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany
{
    public interface IPartnerCompanyService
    {
        Task<bool> CreateCompany(PartnerCompanyDto partnerCompanyDto);
        Task<List<PartnerCompanyView>> GetAllCompany();
    }
}
