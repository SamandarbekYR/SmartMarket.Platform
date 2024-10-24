using Newtonsoft.Json;

using NLog;

using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products;

public class SalesRequestServer : ISalesRequestsServer
{
    public async Task<bool> AddAsync(AddSalesRequestDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            dto.WorkerId = TokenHandler.ParseToken(token).Id;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/sales-request"))
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
                        return false;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
    }

    public async Task<IList<SalesRequestDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/sales-request");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            var sales = JsonConvert.DeserializeObject<List<SalesRequestDto>>(response)!;

            return sales;

        }
        catch
        {
            return new List<SalesRequestDto>();
        }
    }

    public async Task<IList<SalesRequestDto>> FilterSalesRequestAsync(FilterSalesRequestDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/sales-request/filter");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(client.BaseAddress, content);

            string response = await message.Content.ReadAsStringAsync();

            var sales = JsonConvert.DeserializeObject<List<SalesRequestDto>>(response)!;

            return sales;
        }
        catch
        {
            return new List<SalesRequestDto>();
        }
    }

    public async Task<SalesRequestDto> GetByIdAsync(Guid Id)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/sales-request/{Id}");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            var sale = JsonConvert.DeserializeObject<SalesRequestDto>(response);

            return sale!;

        }
        catch
        {
            return null!;
        }
    }

    public async Task<bool> UpdateAsync(AddSalesRequestDto dto, Guid Id)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            dto.WorkerId = TokenHandler.ParseToken(token).Id;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + $"/api/sales-request/{Id}"))
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
                        return false;
                    }
                }
            }
        }
        catch
        {
            return false;
        }
    }
}
