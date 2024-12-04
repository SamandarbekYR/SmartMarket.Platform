namespace SmartMarket.Service.DTOs.Order;

public class UpdateOrderProductDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Count { get; set; }
    public int AvailableCount { get; set; }
    public double ItemTotalCost { get; set; }
}
