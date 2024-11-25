using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.PartnerCompany;

public interface IPartnerCompanyService
{
    Task<bool> CreateCompany(AddPartnerCompanyDto partnerCompanyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
    Task<bool> UpdateCompany(Guid id, AddPartnerCompanyDto dto);
    Task<bool> DeleteCompany(Guid id);
}
