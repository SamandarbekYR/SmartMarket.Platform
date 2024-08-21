namespace SmartMarket.Service.DTOs.Order;

public class AddOrderDto
{
    public Guid WorkerId { get; set; }
    public Guid ProductId { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;
    public int Count { get; set; }
}
