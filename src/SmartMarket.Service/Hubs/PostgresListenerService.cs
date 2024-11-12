using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace SmartMarket.Service.Hubs;

public class PostgresListenerService : BackgroundService
{
    private readonly string _connectionString;
    private readonly IHubContext<ShipmentsHub> _hubContext;
    public PostgresListenerService(string connectionString, IHubContext<ShipmentsHub> hubContext)
    {
        _connectionString = connectionString;
        _hubContext = hubContext;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();

        using (var listenCommand = new NpgsqlCommand("LISTEN low_stock;", connection))
        {
            await listenCommand.ExecuteNonQueryAsync(stoppingToken);
        }

        connection.Notification += async (o, e) =>
        {
            var message = e.Payload; 
            Console.Write(message);
            await _hubContext.Clients.All.SendAsync("ReceiveNotification", message, stoppingToken);
        };

        while (!stoppingToken.IsCancellationRequested)
        {
            await connection.WaitAsync(stoppingToken); 
        }
    }
}

