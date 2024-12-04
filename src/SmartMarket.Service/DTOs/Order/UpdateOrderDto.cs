namespace SmartMarket.Service.DTOs.Order;

public class UpdateOrderDto
{
    public Guid WorkerId { get; set; }
    public Guid PartnerId { get; set; }
    public List<UpdateOrderProductDto> ProductOrderItems { get; set; }
}
