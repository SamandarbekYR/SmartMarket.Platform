using Microsoft.AspNetCore.SignalR.Client;
using SmartMarket.Domain.Entities.Orders;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartMarketDeskop.Integrated.Hubs.SendShipMents;

public class SendShipMent : ISendShipMents
{
    HubConnection _hubConnection;
    public Task ShipMent(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task StartConnectionAsync()
    {
        _hubConnection = new HubConnectionBuilder()
              .WithUrl("http://localhost:53353/ShipMentsHub")
              .Build();
        try
        {
            await _hubConnection.StartAsync();
            Console.WriteLine("Hub bilan ulanish amalga oshirildi.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ulanishda xatolik: {ex.Message}");
        }
    }
    public async Task SendShipMentAsync(Order order)
    {
        try
        {
            await _hubConnection.InvokeAsync("SendShipMents", order);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Jo'natma yuborishda xatolik: {ex.Message}");
        }
    }
}
