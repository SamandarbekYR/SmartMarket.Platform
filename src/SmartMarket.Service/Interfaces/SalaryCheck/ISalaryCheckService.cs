using SmartMarket.Service.DTOs.SalaryCheck;

namespace SmartMarket.Service.Interfaces.SalaryCheck;

public interface ISalaryCheckService
{
    Task<bool> AddAsync(AddSalaryCheckDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<SalaryCheckDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddSalaryCheckDto dto, Guid Id);
}
