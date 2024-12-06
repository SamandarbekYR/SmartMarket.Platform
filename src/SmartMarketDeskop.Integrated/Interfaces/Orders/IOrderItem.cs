namespace SmartMarketDeskop.Integrated.Interfaces.Orders;

public interface IOrderItem
{
    Task<bool> DeleteAsync(Guid id);
}
