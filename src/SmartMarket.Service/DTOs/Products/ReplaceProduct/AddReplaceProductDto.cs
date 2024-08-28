namespace SmartMarket.Service.DTOs.Products.ReplaceProduct;

public class AddReplaceProductDto
{
    public Guid ProductSaleId { get; set; }
    public Guid WorkerId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
