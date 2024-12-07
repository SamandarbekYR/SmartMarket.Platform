using SmartMarketDeskop.Integrated.Interfaces.Orders;

namespace SmartMarketDeskop.Integrated.Repositories.Orders;

public class OrderItemRepository : IOrderItem
{
    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
