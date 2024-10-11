using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses
{
    public interface ILoadReportServer
    {
        Task<List<LoadReportView>> GetAllAsync();
    }
}
