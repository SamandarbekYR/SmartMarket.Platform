namespace SmartMarket.Service.DTOs.Products.InvalidProduct;

public class AddInvalidProductDto
{
    public Guid WorkerId { get; set; }
    public Guid ProductSaleId { get; set; }
    public int Count { get; set; }
    public string Reason { get; set; } = string.Empty;
}
