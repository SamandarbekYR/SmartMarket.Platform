namespace SmartMarket.Service.Interfaces.Order;

public interface IOrderItemService
{
    Task<bool> DeleteAsync(Guid id);
}
