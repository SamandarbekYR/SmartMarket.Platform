using Newtonsoft.Json;
using NLog;

using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;

using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products
{
    public class ProductSaleServer : IProductSaleServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<List<ProductSaleViewModel>> GetAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/product-sales");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();

                List<ProductSaleViewModel> products = JsonConvert.DeserializeObject<List<ProductSaleViewModel>>(response)!;

                return products;
            }
            catch
            {
                return new List<ProductSaleViewModel>();
            }
        }
    }
}
