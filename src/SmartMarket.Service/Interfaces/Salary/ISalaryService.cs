using SmartMarket.Service.DTOs.Salary;
using Et = SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.Interfaces.Salary
{
    public interface ISalaryService
    {
        Task<bool> AddAsync(AddSalaryDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<List<SalaryDto>> GetAllAsync();
        Task<bool> UpdateAsync(AddSalaryDto dto, Guid Id);
    }
}