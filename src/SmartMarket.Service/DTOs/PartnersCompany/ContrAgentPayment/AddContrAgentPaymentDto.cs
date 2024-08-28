namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

public class AddContrAgentPaymentDto
{
    public Guid ContrAgentId { get; set; }
    public double TotalDebt { get; set; }
    public double LastPayment { get; set; }
}
