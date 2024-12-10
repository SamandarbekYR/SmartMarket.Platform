using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{
    public class LoadReportServer : ILoadReportServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<bool> AddAsync(AddLoadReportDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                using(var client = new HttpClient())
                using(var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/load-reports"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;

                    var response = await client.SendAsync(request);

                    var result = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return false;
            }
        }

        public async Task<List<CollectedLoadReportDto>> FilterCollectedLoadReportAsync(FilterLoadReportDto dto)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports/filter-collected");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(dto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage message = await client.PostAsync(client.BaseAddress, data);
                string response = await message.Content.ReadAsStringAsync();

                List<CollectedLoadReportDto> collectedLoadReports = JsonConvert.DeserializeObject<List<CollectedLoadReportDto>>(response)!;

                return collectedLoadReports;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return new List<CollectedLoadReportDto>();
            }
        }

        public async Task<List<LoadReportDto>> FilterLoadReportAsync(FilterLoadReportDto dto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports/filter");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(dto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(client.BaseAddress, data);
                string response = await responseMessage.Content.ReadAsStringAsync();

                List<LoadReportDto> loadReports = JsonConvert.DeserializeObject<List<LoadReportDto>>(response)!;

                return loadReports;
            }
            catch(Exception ex)
            {
                logger.Error(ex);
                return new List<LoadReportDto>();
            }
        }

        public async Task<List<LoadReportDto>> GetAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<LoadReportDto> expenses = JsonConvert.DeserializeObject<List<LoadReportDto>>(response)!;

                return expenses;
            }
            catch (Exception ex)
            {
                return new List<LoadReportDto>();
            }
        }

        public async Task<List<CollectedLoadReportDto>> GetAllCollectedAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports/collected");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                var response = await message.Content.ReadAsStringAsync();
                List<CollectedLoadReportDto> loadReports = JsonConvert.DeserializeObject<List<CollectedLoadReportDto>>(response)!;

                return loadReports;
            }
            catch(Exception ex)
            {
                return new List<CollectedLoadReportDto>();
            }
        }

        public async Task<List<LoadReportDto>> GetLoadReportsByContrAgentIdAsync(Guid contrAgentId)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports/contr-agent/{contrAgentId}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<LoadReportDto> loadReports = JsonConvert.DeserializeObject<List<LoadReportDto>>(response)!;

                return loadReports;
            }
            catch(Exception ex)
            {
                return new List<LoadReportDto>();
            }
        }

        public async Task<LoadReportStatisticsDto> GetLoadReportStatisticsAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports/statistics");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                LoadReportStatisticsDto loadReportStatistics = JsonConvert.DeserializeObject<LoadReportStatisticsDto>(response)!;

                return loadReportStatistics;
            }
            catch(Exception ex)
            {
                return new LoadReportStatisticsDto();
            }
        }
    }
}
