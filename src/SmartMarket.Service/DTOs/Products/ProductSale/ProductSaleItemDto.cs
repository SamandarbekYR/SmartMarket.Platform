namespace SmartMarket.Service.DTOs.Products.ProductSale;

public class ProductSaleItemDto
{
    public Guid ProductId { get; set; }
    public int Count { get; set; }
    public double TotalCost { get; set; }
}
