using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Interfaces.Workers;

public interface ISalaryCheck : IRepository<SalaryCheckView>
{
    public Task<List<SalaryCheckView>> GetSalaryChecksFullInformationAsync();
}
