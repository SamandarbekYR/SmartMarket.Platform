using Microsoft.AspNetCore.SignalR;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.Service.Hubs;

public class ShipmentsHub : Hub
{
    public async Task SendShipMents(Order order)
    {
        await Clients.All.SendAsync("ReceiveShipMents", order);
    }

    public async Task SendNotification(string name, int count)
    {
        await Clients.All.SendAsync("ReceiveNotification", name, count);
    }
}