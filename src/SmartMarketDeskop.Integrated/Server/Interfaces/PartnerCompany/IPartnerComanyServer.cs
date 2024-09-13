using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IPartnerComanyServer
{
    Task<bool> AddCompany(PartnerCompanyDto companyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
}
