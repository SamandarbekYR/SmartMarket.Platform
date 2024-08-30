using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Expenses;

namespace SmartMarketDeskop.Integrated.Repositories.Expenses
{
    public class ExpenseRepository : Repository<ExpenseView>, IExpense 
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ExpenseView> _expenses;

        public ExpenseRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _expenses = appDb.Set<ExpenseView>();
        }

        public async Task<List<ExpenseView>> GetExpensesFullInformationAsync()
        {
            return await _expenses
                .Include(e => e.WorkerViewId)
                .Include(e => e.PayDeskViewId)
                .ToListAsync();
        }
    }
}
