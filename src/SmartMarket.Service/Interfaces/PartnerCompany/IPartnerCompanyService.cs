using SmartMarket.Service.DTOs.PartnerCompany;

namespace SmartMarket.Service.Interfaces.PartnerCompany;

public interface IPartnerCompanyService
{
    Task<bool> AddAsync(AddPartnerCompanyDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PartnerCompanyDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddPartnerCompanyDto dto, Guid Id);
}
