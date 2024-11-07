using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.DTOs.Products.LoadReport;

public class CollectedLoadReportDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public Guid ProductId { get; set; }
    public Et.Product Product { get; set; }
    public string Transaction { get; set; }
    public double TotalPrice { get; set; }
    public string ProductName { get; set; }
    public int ProductCount { get; set; }
    public string ContrAgentName { get; set; }
    public DateTime? CreatedDate { get; set; }
}
