using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Interfaces.Workers
{
    public interface ISalaryCheck : IRepository<SalaryCheck>
    {
        public Task<List<SalaryCheck>> GetSalaryChecksFullInformationAsync();
    }
}
