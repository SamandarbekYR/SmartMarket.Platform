namespace SmartMarket.Service.DTOs.Products.ProductSale;

public class AddProductSaleDto
{
    public Guid WorkerId { get; set; }
    public Guid TransactionId { get; set; }
    public Guid PayDeskId { get; set; }
    public long TransactionNumber { get; set; }
    public List<ProductSaleItemDto> ProductItems { get; set; } = new List<ProductSaleItemDto>();
    public double CashSum { get; set; }
    public string CardSum { get; set; } = string.Empty;
    public double TransferMoney { get; set; }
    public double DebtSum { get; set; }
}
