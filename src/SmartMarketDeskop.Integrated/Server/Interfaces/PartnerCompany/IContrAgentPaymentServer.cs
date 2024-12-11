using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IContrAgentPaymentServer
{
    Task<bool> AddAsync(AddContrAgentPaymentDto dto);
    Task<List<ContrAgentPaymentDto>> FilterContrAgentPaymentsAsync(FilterContrAgentDto dto);
    Task<bool> UpdateAsync(AddContrAgentPaymentDto dto);
    Task<List<ContrAgentPaymentDto>> GetAllByContrAgentIdAsync(Guid Id);
}
