using SmartMarket.Service.DTOs.Workers;
using SmartMarket.Service.Interfaces.Workers;

namespace SmartMarket.Service.Services.Workers;

public class WorkerService : IWorkerService
{
    public Task<bool> AddAsync(AddWorkerDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<List<WorkerDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(AddWorkerDto dto, Guid Id)
    {
        throw new NotImplementedException();
    }
}
