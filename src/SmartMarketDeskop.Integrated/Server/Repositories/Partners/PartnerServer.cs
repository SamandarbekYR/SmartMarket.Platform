using Newtonsoft.Json;
using NLog;
using SmartMarket.Domain.Entities.Partners;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Partners;
using SmartMarketDesktop.DTOs.DTOs.Partners;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Partners;

public class PartnerServer : IPartnerServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public async Task<bool> AddAsync(PartnerCreateDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/partners"))
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
                        logger.Error($"Failed to add Partner. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("PartnerServer: AddAsync method exception: ", ex);
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid Id)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/partners/{Id}");

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

    public async Task<List<Partner>> GetAllAsync()
    {
        HttpClient client = new HttpClient();
        var token = IdentitySingelton.GetInstance().Token;

        client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/partners");

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var result = await client.GetAsync(client.BaseAddress);

        string response = await result.Content.ReadAsStringAsync();

        var partners = JsonConvert.DeserializeObject<List<Partner>>(response);

        return partners!;
    }

    public async Task<bool> UpdateAsync(PartnerCreateDto dto, Guid Id)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var workerId = TokenHandler.ParseToken(token).Id;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "api/partners"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(dto.FirstName), "FirstName");
                    content.Add(new StringContent(dto.LastName), "LastName");
                    content.Add(new StringContent(dto.PhoneNumber), "PhoneNumber");

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
            logger.Error("PartnerServer: UpdateAsync method exception: ", ex);
            return false;
        }
    }
}
