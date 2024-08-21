namespace SmartMarket.Service.DTOs.Order;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public Guid ProductId { get; set; }
    public string TransactionNumber { get; set; } = string.Empty;
    public int Count { get; set; }
}
