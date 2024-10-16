using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Worker
{
    public interface IWorkerService 
    {
        Task<bool> CreateAsync(AddWorkerDto dto);
        Task<bool> UpdateAsync(AddWorkerDto worker, Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        Task<List<WorkerDto>> GetAllAsync();
        Task<List<WorkerDto>> GetWorkerByName(string name);
    }
}
