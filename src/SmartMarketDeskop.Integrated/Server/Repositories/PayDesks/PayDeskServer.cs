using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.PayDesks;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.PayDesks;

public class PayDeskServer : IPayDesksServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public async Task<bool> AddAsync(AddPayDesksDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/pay-desks"))
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
                        logger.Error($"Failed to add PayDesk. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("PayDeskServer: AddAsync method exception: ", ex);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/pay-desks/{id}");

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

    public async Task<List<PayDesksDto>> GetAllAsync()
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/pay-desks");

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

            string response = await message.Content.ReadAsStringAsync();

            List<PayDesksDto> paydesks = JsonConvert.DeserializeObject<List<PayDesksDto>>(response)!;

            return paydesks;

        }
        catch
        {
            return new List<PayDesksDto>();
        }
    }

    public async Task<bool> UpdateAsync(AddPayDesksDto dto, Guid id)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Put, AuthApi.BASE_URL + $"/api/pay-desks/{id}"))
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
                        logger.Error($"Failed to update PayDesk. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("PayDeskServer: UpdateAsync method exception: ", ex);
            return false;
        }
    }
}
