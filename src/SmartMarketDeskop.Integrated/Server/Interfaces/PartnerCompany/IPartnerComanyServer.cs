using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IPartnerComanyServer
{
    Task<bool> AddCompany(AddPartnerCompanyDto companyDto);
    Task<List<PartnerCompanyView>> GetAllCompany();
}
