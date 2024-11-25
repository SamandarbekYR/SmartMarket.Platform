using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IPartnerComanyServer
{
    Task<bool> AddCompany(AddPartnerCompanyDto companyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
    Task<bool> UpdateCompany(Guid id, AddPartnerCompanyDto dto);
    Task<bool> DeleteCompany(Guid id);
}
