using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Interfaces.Workers
{
    public interface IWorkerDebt : IRepository<WorkerDebt>
    {
        public Task<List<WorkerDebt>> GetWorkerDebtsFullInformationAsync();
    }
}
