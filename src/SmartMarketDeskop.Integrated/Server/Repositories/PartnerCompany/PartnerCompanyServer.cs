using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany
{
    public class PartnerCompanyServer : IPartnerComanyServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<bool> AddCompany(AddPartnerCompanyDto companyDto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/common/partner-companies"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(companyDto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;

                    var response = await client.SendAsync(request);

                    // Read response content
                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        // Log the response status and content for debugging
                        logger.Error($"Failed to add Contr Agent. Status Code: {response.StatusCode}, Response: {resultContent}");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ContrAgentServer: AddAsync method exception: ", ex);
                return false;
            }
        }

        public async Task<bool> DeleteCompany(Guid id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/partner-companies/{id}");

                var token = IdentitySingelton.GetInstance().Token;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var result = await client.DeleteAsync(client.BaseAddress);

                if (result.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<PartnerCompanyView>> GetAllCompany()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;

                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/partner-companies");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                string response = await message.Content.ReadAsStringAsync();

                List<PartnerCompanyView> posts = JsonConvert.DeserializeObject<List<PartnerCompanyView>>(response)!;

                return posts;
            }
            catch
            {
                return new List<PartnerCompanyView>();
            }
        }

        public async Task<bool> UpdateCompany(Guid id, AddPartnerCompanyDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var client = new HttpClient();
                var url = $"{AuthApi.BASE_URL}/api/common/partner-companies/{id}";
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Headers.Add("Authorization", $"Bearer {token}");

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
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
