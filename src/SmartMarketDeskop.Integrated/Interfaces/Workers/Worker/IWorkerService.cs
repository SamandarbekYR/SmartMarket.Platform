using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Workers.Worker
{
    public interface IWorkerService
    {
        Task<bool> CreateWorker(WorkerView workerView);
        Task<bool> UpdateWorker(WorkerView workerView);
        Task<bool> DeleteWorker(Guid Id);
        Task<List<WorkerView>> GetAllWorker();
    }
}
