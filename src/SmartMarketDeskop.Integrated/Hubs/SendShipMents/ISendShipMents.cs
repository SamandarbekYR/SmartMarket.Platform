using SmartMarket.Domain.Entities.Orders;
using SmartMarketDesktop.ViewModels.Entities.Orders;

namespace SmartMarketDeskop.Integrated.Hubs.SendShipMents;

public interface ISendShipMents
{
    Task StartConnectionAsync();
    Task SendShipMentAsync(Order order);
}
