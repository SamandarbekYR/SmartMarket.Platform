using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

namespace SmartMarketDeskop.Integrated.Services.PartnerCompanies.ContrAgentPayments;

public interface IContrAgentPaymentService
{
    Task<bool> AddAsync(AddContrAgentPaymentDto dto);
    Task<bool> UpdateAsync(AddContrAgentPaymentDto dto);
    Task<List<ContrAgentPaymentDto>> GetAllByContrAgentIdAsync(Guid Id);
}
