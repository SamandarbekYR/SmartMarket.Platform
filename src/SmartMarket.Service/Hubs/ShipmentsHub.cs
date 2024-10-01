using Microsoft.AspNetCore.SignalR;
using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.Service.Hubs;

public class ShipmentsHub : Hub
{
    public async Task SendShipMents(Order order)
    {
        await Clients.All.SendAsync("ReceiveShipMents", order);
    }
}