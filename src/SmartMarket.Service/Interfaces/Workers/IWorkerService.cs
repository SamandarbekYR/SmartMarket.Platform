using SmartMarket.Service.DTOs.Workers;

namespace SmartMarket.Service.Interfaces.Workers;

internal interface IWorkerService
{
    Task<bool> AddAsync(AddWrokerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<WorkerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddWrokerDto dto, Guid Id);
}
