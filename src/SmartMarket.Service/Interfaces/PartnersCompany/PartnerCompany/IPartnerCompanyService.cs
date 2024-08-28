using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;

namespace SmartMarket.Service.Interfaces.PartnersCompany.PartnerCompany;

public interface IPartnerCompanyService
{
    Task<bool> AddAsync(AddPartnerCompanyDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PartnerCompanyDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddPartnerCompanyDto dto, Guid Id);
}
