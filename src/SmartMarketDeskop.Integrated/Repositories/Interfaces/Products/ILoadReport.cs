using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface ILoadReport : IRepository<LoadReportView>
{
    public Task<List<LoadReportView>> GetLoadReportsFullInformationAsync();
}
