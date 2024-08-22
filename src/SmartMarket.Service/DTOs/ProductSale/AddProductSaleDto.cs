namespace SmartMarket.Service.DTOs.ProductSale;

public class AddProductSaleDto
{
    public Guid ProductId { get; set; }
    public Guid WorkerId { get; set; }
    public Guid TransactionId { get; set; }
    public Guid PayDeskId { get; set; }
    public long TransactionNumber { get; set; }
    public int Count { get; set; }
    public double TotalCost { get; set; }
    public double CashSum { get; set; }
    public string CardSum { get; set; } = string.Empty;
    public double TransferMoney { get; set; }
    public double DebtSum { get; set; }
}
