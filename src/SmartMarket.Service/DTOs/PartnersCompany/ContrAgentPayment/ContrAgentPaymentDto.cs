namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

public class ContrAgentPaymentDto
{
    public Guid Id { get; set; }
    public Guid ContrAgentId { get; set; }
    public Guid PayDeskId { get; set; }
    public string PaymentType { get; set; }
    public double TotalDebt { get; set; }
    public double LastPayment { get; set; }
    public DateTime LastPaymentDate { get; set; }
}
