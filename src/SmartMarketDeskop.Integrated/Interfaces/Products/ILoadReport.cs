using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface ILoadReport : IRepository<LoadReportView>
{
    public Task<List<LoadReportView>> GetLoadReportsFullInformationAsync();
}
