namespace SmartMarketDesktop.DTOs.DTOs.Transactions;

public class TransactionDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;
    public double Price { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
    public int AvailableCount { get; set; }
    public float Discount { get; set; }
    public double DiscountSum { get; set; }
}
