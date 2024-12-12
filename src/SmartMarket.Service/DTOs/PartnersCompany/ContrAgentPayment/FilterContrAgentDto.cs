namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

public class FilterContrAgentDto
{
    public Guid Id { get; set; }
    public Guid? PayDeskId { get; set; }
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
}
