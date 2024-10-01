using SmartMarket.Domain.Entities.Orders;

namespace SmartMarketDeskop.Integrated.Hubs.ReceiveShipments
{
    public interface IReceiveShipMent
    {
        Task StartConnectionAsync();
        Task ReceiveShipMentAsync(Action<Order> onOrderReceived);
    }
}
