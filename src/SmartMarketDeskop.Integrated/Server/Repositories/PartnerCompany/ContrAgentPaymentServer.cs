using Newtonsoft.Json;
using NLog;

using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany;

public class ContrAgentPaymentServer : IContrAgentPaymentServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    public async Task<bool> AddAsync(AddContrAgentPaymentDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/common/contr-agent-payments"))
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
                    logger.Error($"Failed to add contr agent payment. Status code: {response.StatusCode}, Response: {resultContent}");
                }

                return false;
            }
        }
        catch(Exception ex)
        {
            logger.Error("ContrAgentPaymentServer: AddAsync method exception: ", ex);
            return false;
        }
    }

    public async Task<List<ContrAgentPaymentDto>> FilterContrAgentPaymentsAsync(FilterContrAgentDto dto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;

            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/contr-agent-payments/filter");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(dto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage message = await client.PostAsync(client.BaseAddress, data);
            string response = await message.Content.ReadAsStringAsync();

            var contrAgentPaymentDtos = JsonConvert.DeserializeObject<List<ContrAgentPaymentDto>>(response)!;

            return contrAgentPaymentDtos;
        }
        catch(Exception ex)
        {
            return new List<ContrAgentPaymentDto>(); 
        }
    }

    public async Task<List<ContrAgentPaymentDto>> GetAllByContrAgentIdAsync(Guid Id)
    {
        HttpClient client = new HttpClient();
        var token = IdentitySingelton.GetInstance().Token;

        client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/common/contr-agent-payments/contr-agent/{Id}");

        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        var result = await client.GetAsync(client.BaseAddress);

        string response = await result.Content.ReadAsStringAsync();

        var contrAgentPayments = JsonConvert.DeserializeObject<List<ContrAgentPaymentDto>>(response);

        return contrAgentPayments!;
    }

    public async Task<bool> UpdateAsync(AddContrAgentPaymentDto dto)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/common/contr-agent-payments/update"))
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
                    logger.Error($"Failed to add contr agent payment. Status code: {response.StatusCode}, Response: {resultContent}");
                }

                return false;
            }
        }
        catch (Exception ex)
        {
            logger.Error("ContrAgentPaymentServer: AddAsync method exception: ", ex);
            return false;
        }
    }
}
