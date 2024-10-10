using Newtonsoft.Json;

using NLog;

using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;

using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;

using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products
{
    public class InvalidProductServer : IInvalidProductServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddAsync(AddReplaceProductDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/invalid-products"))
                    {
                        request.Headers.Add("Authorization", $"Bearer {token}");

                        var json = JsonConvert.SerializeObject(dto);
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
                            logger.Error($"Failed to add Invalid Product. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to add Invalid Product.");
                return false;
            }
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<InvalidProductDto>> GetAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/invalid-products");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();
                List<InvalidProductDto> replaceProducts = JsonConvert.DeserializeObject<List<InvalidProductDto>>(response)!;

                return replaceProducts;
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to get all Invalid Products.");
                return new List<InvalidProductDto>();
            }
        }

        public Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
