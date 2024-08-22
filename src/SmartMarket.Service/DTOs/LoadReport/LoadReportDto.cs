namespace SmartMarket.Service.DTOs.LoadReport;

public class LoadReportDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public Guid ProductId { get; set; }
    public Guid ContrAgentId { get; set; }
    public double TotalPrice { get; set; }
}
