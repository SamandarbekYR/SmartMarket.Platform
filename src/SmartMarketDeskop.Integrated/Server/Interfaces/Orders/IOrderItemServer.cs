namespace SmartMarketDeskop.Integrated.Server.Interfaces.Orders;

public interface IOrderItemServer
{
    Task<bool> Delete(Guid id);
}
