namespace SmartMarket.Service.DTOs.ContrAgentPayment;

public class ContrAgentPaymentDto
{
    public Guid Id { get; set; }
    public Guid ContrAgentId { get; set; }
    public double TotalDebt { get; set; }
    public double LastPayment { get; set; }
    public DateTime LastPaymentDate { get; set; }
}
