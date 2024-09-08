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
        Task<bool> CreateProduct(WorkerDto dto);
        Task<bool> UpdateProduct(WorkerDto worker, Guid Id);
        Task<bool> DeleteProduct(Guid Id);
       
    }
}
