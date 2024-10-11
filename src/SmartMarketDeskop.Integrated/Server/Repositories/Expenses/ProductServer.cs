using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{
    public class ProductServer : IProductServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<List<ProductView>> GetAllProduct()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/products");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                string response = await message.Content.ReadAsStringAsync();

                List<ProductView> products = JsonConvert.DeserializeObject<List<ProductView>>(response)!;

                return products;

            }
            catch
            {
                return new List<ProductView>();
            }
        }
    }
}
