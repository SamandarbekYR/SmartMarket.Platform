using Et = SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Workers.Worker;

namespace SmartMarket.Service.Interfaces.Worker.Workers;

public interface IWorkerService
{
    Task<bool> AddAsync(AddWorkerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<Et.Worker>> GetAllAsync();
    Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id);

    Task<WorkerDto> GetWorkerByPhoneNumberAsync(string phoneNumber);
    Task<WorkerDto> GetWorkerByNameAsync(string phoneNumber);
}
