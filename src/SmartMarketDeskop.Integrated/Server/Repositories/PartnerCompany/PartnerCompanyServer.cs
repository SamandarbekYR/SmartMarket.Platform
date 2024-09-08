using Newtonsoft.Json;
using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany
{
    public class PartnerCompanyServer : IPartnerComanyServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<bool> AddCompany(PartnerCompanyDto companyDto)
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

        public async Task<List<PartnerCompanyView>> GetAllCompany()
        {
            try
            {
                // HttpClient yaratish
                HttpClient client = new HttpClient();

                // Tokenni qo'shish
                var token = IdentitySingelton.GetInstance().Token;

                // API so'rovining bazaviy manzilini sozlash
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/partner-companies");

                // Authorization headerni qo'shish
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Get so'rovini amalga oshirish
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                // So'rov natijasini o'qish
                string response = await message.Content.ReadAsStringAsync();

                // JSON ni CategoryView listiga deserialize qilish
                List<PartnerCompanyView> posts = JsonConvert.DeserializeObject<List<PartnerCompanyView>>(response)!;

                return posts;
            }
            catch
            {
                // Xatolik yuz bergan taqdirda bo'sh ro'yxat qaytarish
                return new List<PartnerCompanyView>();
            }
        }
    }
}
