using SmartMarket.Service.DTOs.Workers.SalaryWorker;

namespace SmartMarket.Service.Interfaces.Worker.SalaryWorker;

public interface ISalaryWorkerService
{
    Task<bool> AddAsync(AddSalaryWorkerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<SalaryWorkerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddSalaryWorkerDto dto, Guid Id);
}
