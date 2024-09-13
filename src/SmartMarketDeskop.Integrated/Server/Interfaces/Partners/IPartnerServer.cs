using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Partners;

public interface IPartnerServer
{
    Task<List<Partner>> GetAllAsync();
    Task<bool> AddAsync(AddPartnerDto dto);

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(AddPartnerDto dto, Guid Id);
}
