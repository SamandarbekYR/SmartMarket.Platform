namespace SmartMarket.Service.DTOs.ReplaceProduct;

public class ReplaceProductDto
{
    public Guid Id { get; set; }
    public Guid ProductSaleId { get; set; }
    public Guid WorkerId { get; set; }
    public string Reason { get; set; } = string.Empty;
}
