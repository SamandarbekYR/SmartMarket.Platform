using Newtonsoft.Json;
using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Expenses;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{

    public class ExpensesServer : IExpensesServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<List<ExpenseView>> GetExpensesFullInformationAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/expences");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<ExpenseView> expenses = JsonConvert.DeserializeObject<List<ExpenseView>>(response)!;

                return expenses;
            }
            catch (Exception ex)
            {
                return new List<ExpenseView>();
            }
        }
    }
}
