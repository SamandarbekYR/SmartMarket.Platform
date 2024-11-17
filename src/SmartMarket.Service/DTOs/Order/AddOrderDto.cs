namespace SmartMarket.Service.DTOs.Order;

public class AddOrderDto
{
    public Guid WorkerId { get; set; }
    public Guid PartnerId { get; set; }
    public List<AddOrderProductDto> ProductOrderItems { get; set; }
}
