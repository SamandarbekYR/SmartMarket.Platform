using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;
using SmartMarketDesktop.DTOs.DTOs.Partners;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Partners;

public interface IPartnerServer
{
    Task<List<Partner>> GetAllAsync();
    Task<bool> AddAsync(PartnerCreateDto dto);
    Task<PartnerDto> GetByNameAsync(string name);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(PartnerCreateDto dto, Guid Id);
    Task<bool> UpdateDebtSumAsync(double debtSum, Guid Id);
}
