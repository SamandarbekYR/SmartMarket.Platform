using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Workers.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Workers
{
    public interface IWorkerServer
    {
        Task<List<Worker>> GetAllAsync();
        Task<bool> AddAsync(WorkerDto dto);

        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(WorkerDto dto, Guid Id);
    }
}
