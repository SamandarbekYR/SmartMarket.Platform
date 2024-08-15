using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Expenses;
using SmartMarket.Domain.Entities.Expenses;

namespace SmartMarket.DataAccess.Repositories.Experses
{
    public class ExpenseRepository : Repository<Expense>, IExpense
    {
        public ExpenseRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
