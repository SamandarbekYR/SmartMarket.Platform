using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
using SmartMarketDesktop.DTOs.DTOs.Partners;

namespace SmartMarketDeskop.Integrated.Services.Partners;

public interface IPartnerService
{
    Task<bool> CreatePartner(PartnerCreateDto dto);
    Task<bool> UpdatePartner(PartnerCreateDto partner, Guid Id);
    Task<bool> UpdatePartnerDebtSum(double debtSum, Guid Id);
    Task<bool> DeletePartner(Guid Id);
    Task<List<Partner>> GetAll();
    Task<PartnerDto> GetByName(string name);
}
