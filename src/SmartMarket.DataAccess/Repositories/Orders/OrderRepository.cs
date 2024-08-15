using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Orders;
using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.DataAccess.Repositories.Orders
{
    public class OrderRepository : Repository<Order>, IOrder
    {
        public OrderRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}