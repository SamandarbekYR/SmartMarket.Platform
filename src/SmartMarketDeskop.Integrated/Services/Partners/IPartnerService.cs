using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.Partner;

namespace SmartMarketDeskop.Integrated.Services.Partners;

public interface IPartnerService
{
    Task<bool> CreateProduct(AddPartnerDto dto);
    Task<bool> UpdateProduct(AddPartnerDto partner, Guid Id);
    Task<bool> DeleteProduct(Guid Id);
    Task<List<Partner>> GetAll();
}
