using SmartMarket.Service.DTOs.Workers.WorkerRole;

namespace SmartMarket.Service.Interfaces.Worker.WorkerRole;

public interface IWorkerRoleService
{
    Task<bool> AddAsync(AddWorkerRoleDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<WorkerRoleDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddWorkerRoleDto dto, Guid Id);
}
