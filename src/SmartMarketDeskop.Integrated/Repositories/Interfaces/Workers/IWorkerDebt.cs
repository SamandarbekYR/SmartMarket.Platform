using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;

public interface IWorkerDebt : IRepository<WorkerDebtView>
{
    public Task<List<WorkerDebtView>> GetWorkerDebtsFullInformationAsync();
}
