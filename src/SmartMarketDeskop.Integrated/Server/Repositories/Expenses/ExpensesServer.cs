using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{

    public class ExpensesServer : IExpensesServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public async Task<List<FullExpenceDto>> FilterExpensesAsync(FilterExpenseDto dto)
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/expences/filter");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var json = JsonConvert.SerializeObject(dto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage responseMessage = await client.PostAsync(client.BaseAddress, data);
                string response = await responseMessage.Content.ReadAsStringAsync();

                List<FullExpenceDto> expences = JsonConvert.DeserializeObject<List<FullExpenceDto>>(response)!;

                return expences;
            }
            catch (Exception ex)
            {
                return new List<FullExpenceDto>();
            }
        }

        public async Task<List<FullExpenceDto>> GetExpensesByReasonAsync(string reason)
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/expences/reason/{reason}");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<FullExpenceDto> expenses = JsonConvert.DeserializeObject<List<FullExpenceDto>>(response)!;

                return expenses;
            }
            catch (Exception ex)
            {
                return new List<FullExpenceDto>();
            }
        }

        public async Task<List<FullExpenceDto>> GetExpensesFullInformationAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/expences");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<FullExpenceDto> expenses = JsonConvert.DeserializeObject<List<FullExpenceDto>>(response)!;

                return expenses;
            }
            catch (Exception ex)
            {
                return new List<FullExpenceDto>();
            }
        }
    }
}
