using SmartMarket.Domain.Entities.Expenses;

namespace SmartMarket.DataAccess.Interfaces.Expenses
{
    public interface IExpense : IRepository<Expense>
    {
        public Task<List<Expense>> GetExpensesFullInformationAsync();
    }
}
