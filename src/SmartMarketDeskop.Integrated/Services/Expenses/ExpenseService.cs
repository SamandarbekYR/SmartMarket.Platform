using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Expenses
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpensesServer _expensesServer;
        public ExpenseService()
        {
            this._expensesServer = new ExpensesServer();
        }

        public async Task<bool> CreateExpense(AddExpenceDto dto)
        {
            if (IsInternetAvialable())
            {
                bool result = await _expensesServer.CreateAsync(dto);
                if (result)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<FullExpenceDto>> FilterExpense(FilterExpenseDto dto)
        {
            if(IsInternetAvialable())
            {
                var expenses = await _expensesServer.FilterExpenceAsync(dto);
                return expenses;
            }
            else
            {
                return new List<FullExpenceDto>();
            }
        }

        public async Task<List<FullExpenceDto>> GetAll()
        {
            if(IsInternetAvialable())
            {
                var expenses = await _expensesServer.GetExpensesFullInformationAsync();
                return expenses;
            }
            else
            {  return new List<FullExpenceDto>(); }
        }

        public async Task<ExpenseSummaryDto> GetExpenseSummary()
        {
            if(IsInternetAvialable())
            {
                var expenseSummary = await _expensesServer.GetExpenseSummaryAsync();
                return expenseSummary;
            }
            else {  return new ExpenseSummaryDto(); }
        }

        public bool IsInternetAvialable()
        {
            try
            {
                using (var client = new WebClient()!)
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
