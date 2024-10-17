using SmartMarket.Service.DTOs.Expence;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;

public interface IExpensesServer
{
    Task<List<FullExpenceDto>> GetExpensesFullInformationAsync();
    Task<List<FullExpenceDto>>  FilterExpenceAsync(FilterExpenseDto filterExpenceDto);
    Task<bool> CreateAsync(AddExpenceDto expence);
}
