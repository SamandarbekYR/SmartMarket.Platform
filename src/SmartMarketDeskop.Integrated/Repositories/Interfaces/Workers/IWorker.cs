using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;

public interface IWorker : IRepository<WorkerView>
{
    public Task<WorkerView?> GetPhoneNumberAsync(string phoneNumber);
    public Task<List<WorkerView>> GetWorkersFullInformationAsync();
}
