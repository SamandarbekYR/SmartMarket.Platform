using Microsoft.AspNetCore.SignalR;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Order;

namespace SmartMarket.Service.Hubs;

public class ShipmentsHub : Hub
{
    public async Task SendShipMents(string message)
    {
        await Clients.All.SendAsync("ReceiveShipMents", message);
    }

    public async Task SendNotification(string name, int count)
    {
        await Clients.All.SendAsync("ReceiveNotification", name, count);
    }
}