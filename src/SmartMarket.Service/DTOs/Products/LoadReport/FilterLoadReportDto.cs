namespace SmartMarket.Service.DTOs.Products.LoadReport;

public class FilterLoadReportDto
{
    public string? ProductName { get; set; }
    public DateTime? FromDateTime { get; set; }
    public DateTime? ToDateTime { get; set; }
    public string? CompanyName { get; set; }
}
