using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;

public interface IPartnerCompanyService
{
    Task<bool> CreateCompany(PartnerCompanyDto partnerCompanyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
}
