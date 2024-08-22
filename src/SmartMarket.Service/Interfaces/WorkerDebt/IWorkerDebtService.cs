using SmartMarket.Service.DTOs.WorkerDebt;

namespace SmartMarket.Service.Interfaces.WorkerDebt;

public interface IWorkerDebtService
{
    Task<bool> AddAsync(AddWorkerDebtDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<WorkerDebtDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddWorkerDebtDto dto, Guid Id);
}
