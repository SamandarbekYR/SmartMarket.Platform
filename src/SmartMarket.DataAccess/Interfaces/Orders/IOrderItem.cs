using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.DataAccess.Interfaces.Orders;

public interface IOrderItem : IRepository<OrderProduct>
{
}
