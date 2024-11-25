using Newtonsoft.Json;

using NLog;

using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Products.Product;

using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Orders;

using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Orders
{
    public class OrderServer : IOrderServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> CreateAsync(AddOrderDto order)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var workerId = TokenHandler.ParseToken(token).Id;
                order.WorkerId = workerId;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/orders"))
                    {
                        request.Headers.Add("Authorization", $"Bearer {token}");

                        var json = JsonConvert.SerializeObject(order);
                        var content = new StringContent(json, Encoding.UTF8, "application/json");

                        request.Content = content;

                        var response = await client.SendAsync(request);

                        var resultContent = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            logger.Error($"Failed to add Order. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("OrderServer: CreateAsync method exception: ", ex);
                return false;
            }
        }


        public async Task<List<OrderDto>> GetByPartnerNameAsync(string searchName)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/ordera/name/{searchName}");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                string response = await message.Content.ReadAsStringAsync();

                var orders = JsonConvert.DeserializeObject<List<OrderDto>>(response)!;

                return orders;

            }
            catch
            {
                return new List<OrderDto>();
            }
        }

        public async Task<List<OrderDto>> GetAllAsyc()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/orders");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<OrderDto> orders = JsonConvert.DeserializeObject<List<OrderDto>>(response)!;

                return orders;
            }
            catch (Exception ex)
            {
                return new List<OrderDto>();
            }
        }
    }
}
