using SmartMarket.Service.DTOs.SalaryWorker;

namespace SmartMarket.Service.Interfaces.SalaryWorker;

public interface ISalaryWorkerService
{
    Task<bool> AddAsync(AddSalaryWorkerDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<SalaryWorkerDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddSalaryWorkerDto dto, Guid Id);
}
