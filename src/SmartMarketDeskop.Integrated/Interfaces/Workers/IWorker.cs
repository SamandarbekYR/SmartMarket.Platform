using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Interfaces.Workers;

public interface IWorker : IRepository<WorkerView>
{
    public Task<WorkerView?> GetPhoneNumberAsync(string phoneNumber);
    public Task<List<WorkerView>> GetWorkersFullInformationAsync();
}
