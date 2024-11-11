using Microsoft.AspNetCore.SignalR;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Hubs;

public class ShipmentsHub : Hub
{
    public async Task SendShipMents(List<FullProductDto> orders)
    {
        await Clients.All.SendAsync("ReceiveShipMents", orders);
    }

    public async Task SendNotification(string name, int count)
    {
        await Clients.All.SendAsync("ReceiveNotification", name, count);
    }
}