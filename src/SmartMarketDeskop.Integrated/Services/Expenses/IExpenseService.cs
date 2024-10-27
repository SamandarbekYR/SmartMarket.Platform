using SmartMarket.Service.DTOs.Expence;

namespace SmartMarketDeskop.Integrated.Services.Expenses;

public interface IExpenseService
{
    Task<List<FullExpenceDto>> GetAll();
    Task<List<FullExpenceDto>> FilterExpense(FilterExpenseDto dto);
    Task<bool> CreateExpense(AddExpenceDto dto);
    Task<ExpenseSummaryDto> GetExpenseSummary();
}
