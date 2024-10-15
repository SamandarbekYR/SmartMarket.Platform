using SmartMarket.Service.DTOs.Workers.Worker;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Workers
{
    public interface IWorkerServer
    {
        Task<bool> AddAsync(AddWorkerDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id);
        Task<List<WorkerDto>> GetAllAsync();
        Task<List<WorkerDto>> GetWorkerByName(string name);
    }
}
