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
