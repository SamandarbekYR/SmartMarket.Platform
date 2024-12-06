
using SmartMarketDeskop.Integrated.Server.Interfaces.Orders;
using SmartMarketDeskop.Integrated.Server.Repositories.Orders;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Orders;

public class OrderItemService : IOrderItemService
{
    private readonly IOrderItemServer _orderItemServer;

    public OrderItemService()
    {
        _orderItemServer = new OrderItemServer();
    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        if(IsInternetAvialable())
        {
            bool result = await _orderItemServer.Delete(id);
            if (result)
                return true;
            else
                return false;
        }
        else
        {
            return false;
        }
    }

    public bool IsInternetAvialable()
    {
        try
        {
            using (var client = new WebClient()!)
            using (client.OpenRead("http://google.com"))
                return true;
        }
        catch
        {
            return false;
        }
    }
}
