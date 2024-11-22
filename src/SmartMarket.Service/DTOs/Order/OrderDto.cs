using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Workers;
using Et = SmartMarket.Domain.Entities.Partners;
namespace SmartMarket.Service.DTOs.Order;

public class OrderDto
{
    public Guid Id { get; set; }
    public long TransactionId { get; set; }
    public Guid WorkerId { get; set; }
    public Worker Worker { get; set; }
    public Guid PartnerId { get; set; }
    public Et.Partner Partner { get; set; }
    public List<OrderProduct> ProductOrderItems { get; set; }
}
