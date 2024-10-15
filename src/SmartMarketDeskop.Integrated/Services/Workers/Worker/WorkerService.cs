using SmartMarket.Service.DTOs.Workers.Worker;

using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Worker
{
    public class WorkerService : IWorkerService
    {
       private IWorkerServer _workerServer;

        public WorkerService()
        {
            _workerServer = new WorkerServer();
        }

        public async Task<bool> CreateAsync(AddWorkerDto dto)
        {
            if (IsInternetAvailable())
            {
                return await _workerServer.AddAsync(dto);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            if (IsInternetAvailable())
            {
                return await _workerServer.DeleteAsync(Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<WorkerDto>> GetAllAsync()
        {
            if (IsInternetAvailable())
            {
                return await _workerServer.GetAllAsync();
            }
            else
            {
                return new List<WorkerDto>();
            }
        }

        public async Task<List<WorkerDto>> GetWorkerByName(string name)
        {
            if (IsInternetAvailable())
            {
                return await _workerServer.GetWorkerByName(name);
            }
            else
            {
                return new List<WorkerDto>();
            }
        }

        public async Task<bool> UpdateAsync(AddWorkerDto worker, Guid Id)
        {
            if (IsInternetAvailable())
            {
                return await _workerServer.UpdateAsync(worker, Id);
            }
            else
            {
                return false;
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
