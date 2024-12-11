using SmartMarket.Domain.Entities.PayDesks;
using Et = SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

public class ContrAgentPaymentDto
{
    public Guid Id { get; set; }
    public Guid ContrAgentId { get; set; }
    public Et.ContrAgent ContrAgent { get; set; }
    public Guid PayDeskId { get; set; }
    public PayDesk PayDesk { get; set; }
    public string PaymentType { get; set; }
    public double TotalDebt { get; set; }
    public double LastPayment { get; set; }
    public DateTime LastPaymentDate { get; set; }
}
