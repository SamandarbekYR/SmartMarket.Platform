using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;

public interface ISalaryCheck : IRepository<SalaryCheckView>
{
    public Task<List<SalaryCheckView>> GetSalaryChecksFullInformationAsync();
}
