using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;

public interface IPartnerCompanyService
{
    Task<bool> CreateCompany(AddPartnerCompanyDto partnerCompanyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
}
