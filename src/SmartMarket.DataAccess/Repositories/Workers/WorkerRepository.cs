using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Expenses;
using SmartMarket.Domain.Entities.Expenses;

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

    public async Task<List<Expense>> GetExpensesFullInformationAsync()
    {
        return await _expenses
            .Include(e => e.Worker)
            .Include(e => e.PayDesk)
            .ToListAsync();
    }
}
