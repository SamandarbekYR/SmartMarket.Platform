using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.DataAccess.Interfaces.Orders
{
    public interface IOrder : IRepository<Order>
    {
        public Task<List<Order>> GetOrdersFullInformationAsync();
        public IQueryable<Order> GetOrdersFull();
    }
}
