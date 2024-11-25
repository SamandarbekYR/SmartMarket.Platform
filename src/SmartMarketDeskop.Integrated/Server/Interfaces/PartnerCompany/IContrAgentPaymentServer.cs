using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;

public interface IContrAgentPaymentServer
{
    Task<bool> AddAsync(AddContrAgentPaymentDto dto);
}
