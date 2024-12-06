namespace SmartMarketDeskop.Integrated.Services.Orders;

public interface IOrderItemService
{
    Task<bool> DeleteAsync(Guid id);
}
