using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Interfaces.Workers
{
    public interface IWorker : IRepository<Worker>
    {
        public Task<Worker?> GetPhoneNumberAsync(string phoneNumber);
        public Task<List<Worker>> GetWorkersFullInformationAsync();
    }
}
