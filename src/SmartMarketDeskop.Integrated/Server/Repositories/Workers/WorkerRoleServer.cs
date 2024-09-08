using Newtonsoft.Json;
using NLog;
using SmartMarket.Domain.Entities.Workers;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Workers
{
    public class WorkerRoleServer : IWorkerRoleServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<List<WorkerRole>> GetAll()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;

                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/super-admin/worker-role");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                string response = await message.Content.ReadAsStringAsync();

                List<WorkerRole> workerrole = JsonConvert.DeserializeObject<List<WorkerRole>>(response)!;

                return workerrole;

            }
            catch
            {
                return new List<WorkerRole>();
            }
        }
    }
}
