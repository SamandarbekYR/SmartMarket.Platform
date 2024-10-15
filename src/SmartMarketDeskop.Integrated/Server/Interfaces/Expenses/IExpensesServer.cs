using SmartMarket.Service.DTOs.Expence;
using SmartMarketDesktop.ViewModels.Entities.Expenses;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses
{
    public interface IExpensesServer
    {
        Task<List<ExpenseView>> GetExpensesFullInformationAsync();
        Task<List<FullExpenceDto>>  FilterExpenceAsync(FilterExpenseDto filterExpenceDto);
    }
}
