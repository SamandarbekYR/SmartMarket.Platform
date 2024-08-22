using SmartMarket.Service.DTOs.Workers;

namespace SmartMarket.Service.Interfaces.Workers;

public interface IWorkerService
{
    Task<bool> AddAsync(AddWorkerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<WorkerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id);
}
