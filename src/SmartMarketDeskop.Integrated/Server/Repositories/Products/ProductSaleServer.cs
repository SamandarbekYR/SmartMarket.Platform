using Newtonsoft.Json;
using NLog;

using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;

using SmartMarketDesktop.ViewModels.Entities.Products;

using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products
{
    public class ProductSaleServer : IProductSaleServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public Task<bool> AddAsync(AddProductSaleDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto productSaleDto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/product-sales/filter");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(productSaleDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PostAsync(client.BaseAddress, data);
                string response = await message.Content.ReadAsStringAsync();

                List<ProductSaleViewModel> products = JsonConvert.DeserializeObject<List<ProductSaleViewModel>>(response)!;

                return products;
            }
            catch
            {
                return new List<ProductSaleViewModel>();
            }
        }

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

        public async Task<ProductSaleViewModel> GetByIdAsync(Guid id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/product-sales/{id}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();

                ProductSaleViewModel product = JsonConvert.DeserializeObject<ProductSaleViewModel>(response)!;

                return product;
            }
            catch
            {
                return new ProductSaleViewModel();
            }
        }

        public async Task<bool> UpdateAsync(AddProductSaleDto dto, Guid id)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);
                    using (var request = new HttpRequestMessage(HttpMethod.Put, AuthApi.BASE_URL + $"/api/product-sales/{id}"))
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
                            logger.Error($"Failed to update Partner. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                        }

                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to update Partner.");
                return false;
            }
        }
    }
}