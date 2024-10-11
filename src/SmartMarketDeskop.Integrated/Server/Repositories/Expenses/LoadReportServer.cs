using Newtonsoft.Json;
using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses
{
    public class LoadReportServer : ILoadReportServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<List<LoadReportView>> GetAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();

                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/load-reports");
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

                string response = await responseMessage.Content.ReadAsStringAsync();
                List<LoadReportView> expenses = JsonConvert.DeserializeObject<List<LoadReportView>>(response)!;

                return expenses;
            }
            catch (Exception ex)
            {
                return new List<LoadReportView>();
            }
        }
    }
}
