using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Expence;

namespace SmartMarket.Service.Interfaces.Expence;

public interface IExpenceService
{
    Task<bool> AddAsync(AddExpenceDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ExpenceDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddExpenceDto dto, Guid Id);

    Task<IEnumerable<ExpenceDto>> GetExpensesByReasonAsync(string reason, PaginationParams paginationParams);
}
