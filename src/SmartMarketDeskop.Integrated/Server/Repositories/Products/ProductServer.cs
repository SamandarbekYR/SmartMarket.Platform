using Newtonsoft.Json;
using NLog;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDesktop.DTOs.DTOs.Product;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products;

public class ProductServer : IProductServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public async Task<bool> AddAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var workerId = TokenHandler.ParseToken(token).Id;
            dto.WorkerId = workerId;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/products"))
                {
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(dto.BarCode), "Barcode");
                    content.Add(new StringContent(dto.P_Code), "PCode");
                    content.Add(new StringContent(dto.ProductName), "Name");
                    content.Add(new StringContent(dto.CategoryId.ToString()), "CategoryId");
                    content.Add(new StringContent(dto.ContrAgentId.ToString()), "ContrAgentId");
                    content.Add(new StringContent(dto.WorkerId.ToString()), "WorkerId");
                    content.Add(new StringContent(dto.Count.ToString()), "Count");
                    content.Add(new StringContent(dto.Price.ToString()), "Price");
                    content.Add(new StringContent(dto.SellPrice.ToString()), "SellPrice");
                    content.Add(new StringContent(dto.SellPricePercantage.ToString()), "SellPricePercantage");
                    content.Add(new StringContent(dto.UnitOfMeasure), "UnitOfMeasure");
                    content.Add(new StringContent(dto.PaymentStatus), "PaymentStatus");
                    content.Add(new StringContent(dto.NoteAmount.ToString()), "NoteAmount");
                    
                    request.Content = content;

                    var response = await client.SendAsync(request);

                    var resultContent = await response.Content.ReadAsStringAsync(); // Read response content

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        logger.Error($"Failed to add Contr Agent. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("ContrAgentServer: AddAsync method exception: ", ex);
            return false;
        }

    }

    public async Task<bool> DeleteAsync(Guid Id)
    {

        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/products/{Id}");

            var token = IdentitySingelton.GetInstance().Token;
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var result = await client.DeleteAsync(client.BaseAddress);

            if (result.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch 
        {
            return false;
        }
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();   
            var token=IdentitySingelton.GetInstance().Token;
            client.BaseAddress=new Uri($"{AuthApi.BASE_URL}/api/products");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response=await message.Content.ReadAsStringAsync();

            List<ProductDto> products=JsonConvert.DeserializeObject<List<ProductDto>>(response)!;

            return products;

        }
        catch
        {
            return new List<ProductDto>();
        }
    }

    public async Task<ProductDto> GetByBarCodeAsync(string barcode)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/products/barcode/{barcode}");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<ProductDto>(response)!;

            return products;
        }
        catch
        {
            return null!;
        } 
    }

    public async Task<List<ProductDto>> GetByCategoryIdAsync(Guid categoryId)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/products/category/{categoryId}");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<ProductDto> products = JsonConvert.DeserializeObject<List<ProductDto>>(response)!;

            return products;

        }
        catch
        {
            return new List<ProductDto>();
        }
    }

    public async Task<List<ProductDto>> GetByPCodeAsync(string PCode)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/products/pcode/{PCode}");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductDto>>(response)!;

            return products;

        }
        catch
        {
            return new List<ProductDto>();
        }
    }

    public async Task<ProductDto> GetByProductNameAsync(string productName)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/products/name/{productName}");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<ProductDto>(response)!;

            return products;

        }
        catch
        {
            return null!;
        }
    }

    public async Task<bool> UpdateAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto, Guid Id)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var client = new HttpClient();
            var url = $"{AuthApi.BASE_URL}/api/products/{Id}";
            var request = new HttpRequestMessage(HttpMethod.Put, url);
            request.Headers.Add("Authorization", $"Bearer {token}");

            // Create JSON content
            var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            request.Content = jsonContent;

            var response = await client.SendAsync(request);

            var res1 = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var res = await response.Content.ReadAsStringAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        catch 
        {
            return false;
        }
    }

}
