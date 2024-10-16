using SmartMarket.Domain.Entities.Workers;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Workers.WorkerRoles
{
    public class WorkerRoleService : IWorkerRoleService
    {
        private IWorkerRoleServer _workerRoleServer;
        public WorkerRoleService()
        {
            _workerRoleServer = new WorkerRoleServer();
        }

        public async Task<List<WorkerRole>> GetAllAsync()
        {
            if (IsInternetAvailable())
            {
                return await _workerRoleServer.GetAllAsync();
            }
            else
            {
                return new List<WorkerRole>();
            }
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient()!)
                using (client.OpenRead("http://google.com"))
                
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
