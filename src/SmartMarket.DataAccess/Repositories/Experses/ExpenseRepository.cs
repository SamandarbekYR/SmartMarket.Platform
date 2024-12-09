using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Expenses;
using SmartMarket.Domain.Entities.Expenses;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Experses;

public class ExpenseRepository : Repository<Expense>, IExpense
{
    private readonly AppDbContext _appDbContext;
    private DbSet<Expense> _expenses;

    public ExpenseRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _expenses = appDb.Set<Expense>();
    }

    public IQueryable<Expense> GetExpensesFullInformationAsync()
    {
        return  _expenses
            .Include(e => e.Worker)
            .Include(e => e.PayDesk);
    }

}
