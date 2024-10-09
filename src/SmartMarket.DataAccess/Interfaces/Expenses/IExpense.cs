using SmartMarket.Domain.Entities.Expenses;

namespace SmartMarket.DataAccess.Interfaces.Expenses
{
    public interface IExpense : IRepository<Expense>
    {
        IQueryable<Expense> GetExpensesFullInformationAsync();
    }
}
