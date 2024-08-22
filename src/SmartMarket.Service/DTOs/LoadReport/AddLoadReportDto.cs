namespace SmartMarket.Service.DTOs.LoadReport;

public class AddLoadReportDto
{
    public Guid WorkerId { get; set; }
    public Guid ProductId { get; set; }
    public Guid ContrAgentId { get; set; }
    public double TotalPrice { get; set; }
}
