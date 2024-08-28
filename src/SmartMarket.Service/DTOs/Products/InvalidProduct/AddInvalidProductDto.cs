namespace SmartMarket.Service.DTOs.Products.InvalidProduct;

public class AddInvalidProductDto
{
    public Guid WorkerId { get; set; }
    public Guid ProductSaleId { get; set; }
    public string ReturnReason { get; set; } = string.Empty;
}
