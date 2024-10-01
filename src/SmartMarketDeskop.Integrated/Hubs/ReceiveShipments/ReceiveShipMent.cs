using Microsoft.AspNetCore.SignalR.Client;
using SmartMarket.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SmartMarketDeskop.Integrated.Hubs.ReceiveShipments
{
    public class ReceiveShipMent : IReceiveShipMent
    {
        private HubConnection _hubConnection;

        public async Task ReceiveShipMentAsync(Action<Order> onOrderReceived)
        {
            _hubConnection.On<Order>("ReceiveShipment", (order) =>
            {
                onOrderReceived?.Invoke(order);
            });
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
    }
}
