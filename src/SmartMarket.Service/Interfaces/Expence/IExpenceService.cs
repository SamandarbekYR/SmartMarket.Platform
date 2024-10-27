using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Expence;

namespace SmartMarket.Service.Interfaces.Expence;

public interface IExpenceService
{
    Task<bool> AddAsync(AddExpenceDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<IEnumerable<FullExpenceDto>> GetAllAsync(PaginationParams @params);
    Task<bool> UpdateAsync(AddExpenceDto dto, Guid Id);
    Task<IEnumerable<FullExpenceDto>> GetExpensesByReasonAsync(string reason, PaginationParams paginationParams);
    Task<IEnumerable<FullExpenceDto>> FilterExpenceAsync(FilterExpenseDto filterExpenseDto);
    Task<FullExpenceDto> GetByIdAsync(Guid Id);
    Task<ExpenseSummaryDto> GetExpenseSummaryAsync();
}
