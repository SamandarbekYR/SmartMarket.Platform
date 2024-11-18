using Microsoft.AspNetCore.SignalR;
using SmartMarket.Domain.Entities.Orders;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Order;

namespace SmartMarket.Service.Hubs;

public class ShipmentsHub : Hub
{
    private readonly IOrderService _orderService;

    public ShipmentsHub(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task SendShipMents(AddOrderDto dto)
    {
        var orders = await _orderService.GetAllAsync();
        await Clients.All.SendAsync("ReceiveShipMents", orders);
    }

    public async Task SendNotification(string name, int count)
    {
        await Clients.All.SendAsync("ReceiveNotification", name, count);
    }
}