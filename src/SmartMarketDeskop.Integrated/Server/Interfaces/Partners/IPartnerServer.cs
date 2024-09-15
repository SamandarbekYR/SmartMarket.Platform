using SmartMarket.Domain.Entities.Partners;
using SmartMarketDesktop.DTOs.DTOs.Partners;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Partners;

public interface IPartnerServer
{
    Task<List<Partner>> GetAllAsync();
    Task<bool> AddAsync(PartnerCreateDto dto);

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(PartnerCreateDto dto, Guid Id);
}
