using SmartMarket.Service.DTOs.Expence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Expenses
{
    public interface IExpenseService
    {
        Task<List<FullExpenceDto>> GetAll();
        Task<List<FullExpenceDto>> FilterExpense(FilterExpenseDto dto);
    }
}
