using SmartMarket.Service.DTOs.Expence;
using SmartMarketDesktop.ViewModels.Entities.Expenses;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses
{
    public interface IExpensesServer
    {
        Task<List<FullExpenceDto>> GetExpensesFullInformationAsync();
    }
}
