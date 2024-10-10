using SmartMarket.Domain.Entities.Workers;
using PS = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Workers.Worker;

namespace SmartMarket.Service.DTOs.Products.InvalidProduct;

public class InvalidProductDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public Guid ProductSaleId { get; set; }
    public PS.ProductSale ProductSale { get; set; }
    public int Count { get; set; }
    public string ReturnReason { get; set; } = string.Empty;
}
