using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;

public interface ISalaryWorker : IRepository<SalaryWorkerView>
{
    public Task<List<SalaryWorkerView>> GetSalaryWorkersFullInformationAsync();
}
