namespace SmartMarket.Service.DTOs.Products.ProductSale;

public class AddProductSaleDto
{
    public Guid ProductId { get; set; }
    public Guid SalesRequestId { get; set; }
    public int Count { get; set; }
    public double Discount { get; set; }
    public double TotalCost { get; set; }
}
