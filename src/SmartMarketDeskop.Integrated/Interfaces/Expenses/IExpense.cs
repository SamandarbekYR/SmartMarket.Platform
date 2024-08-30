using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Expenses
{
    public interface IExpense : IRepository<ExpenseView>
    {
        public Task<List<ExpenseView>> GetExpensesFullInformationAsync();
    }
}
