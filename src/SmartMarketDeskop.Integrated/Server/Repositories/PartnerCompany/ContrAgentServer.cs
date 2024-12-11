using Newtonsoft.Json;
using NLog;
using CT = SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.PartnerCompany;
using SmartMarketDesktop.DTOs.DTOs.PartnerCompany;
using System.Text;
using SmartMarketDeskop.Integrated.ViewModelsForUI.PartnerCompany;

namespace SmartMarketDeskop.Integrated.Server.Repositories.PartnerCompany
{
    public class ContrAgentServer : IContrAgentServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddAsync(ContrAgentDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/common/contr-agents"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);

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

        public async Task<bool> DeleteAsync(Guid Id)
        {

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/common/contr-agents/{Id}");

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
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ContrAgentViewModels>> GetAllAsync()
        {
            try
            {
                // HttpClient yaratish
                HttpClient client = new HttpClient();

                // Tokenni qo'shish
                var token = IdentitySingelton.GetInstance().Token;

                // API so'rovining bazaviy manzilini sozlash
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/contr-agents");

                // Authorization headerni qo'shish
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Get so'rovini amalga oshirish
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                // So'rov natijasini o'qish
                string response = await message.Content.ReadAsStringAsync();

                // JSON ni CategoryView listiga deserialize qilish
                List<ContrAgentViewModels> posts = JsonConvert.DeserializeObject<List<ContrAgentViewModels>>(response)!;

                return posts;
            }
            catch
            {
                // Xatolik yuz bergan taqdirda bo'sh ro'yxat qaytarish
                return new List<ContrAgentViewModels>();
            }

        }

        public async Task<CT.ContrAgentDto> GetByIdAsync(Guid id)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;

                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/contr-agents/{id}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();

                CT.ContrAgentDto contrAgent = JsonConvert.DeserializeObject<CT.ContrAgentDto>(response)!;
                return contrAgent;
            }
            catch(Exception ex)
            {
                return new CT.ContrAgentDto();
            }
        }

        public async Task<List<CT.ContrAgentDto>> GetContrAgentByNameAsync(string name)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;

                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/contr-agents/name/{name}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);
                string response = await message.Content.ReadAsStringAsync();

                List<ContrAgentViewModels> contrAgent = JsonConvert.DeserializeObject<List<ContrAgentViewModels>>(response)!;

                return contrAgent;
            }
            catch(Exception ex)
            {
                return new List<ContrAgentViewModels>();
            }
        }

        public async Task<bool> UpdateAsync(ContrAgentDto dto, Guid Id)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var client = new HttpClient();
                var url = $"{AuthApi.BASE_URL}/api/common/contr-agents/{Id}";
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
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}
