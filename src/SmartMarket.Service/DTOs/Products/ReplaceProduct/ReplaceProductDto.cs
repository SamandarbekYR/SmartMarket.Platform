using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

using PS = SmartMarket.Domain.Entities.Products;
namespace SmartMarket.Service.DTOs.Products.ReplaceProduct;

public class ReplaceProductDto
{
    public Guid Id { get; set; }
    public string? Reason { get; set; }
    public Guid ProductSaleId { get; set; }
    public PS.ProductSale? ProductSale { get; set; }
    public Guid WorkerId { get; set; }
    public Worker? Worker { get; set; }
}
