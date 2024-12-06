using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Orders;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Orders;

public class OrderItemServer : IOrderItemServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public async Task<bool> Delete(Guid id)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthApi.BASE_URL);

            var token = IdentitySingelton.GetInstance().Token;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync($"/api/common/orderItems/{id}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
