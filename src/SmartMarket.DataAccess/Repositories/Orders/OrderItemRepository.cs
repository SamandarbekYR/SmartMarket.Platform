using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Orders;
using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.DataAccess.Repositories.Orders;

public class OrderItemRepository : Repository<OrderProduct>, IOrderItem
{
    public OrderItemRepository(AppDbContext appDb) : base(appDb)
    { }
}
