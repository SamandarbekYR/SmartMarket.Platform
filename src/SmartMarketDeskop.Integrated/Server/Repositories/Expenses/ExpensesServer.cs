using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Expenses;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{

    public class ExpensesServer : IExpensesServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
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
