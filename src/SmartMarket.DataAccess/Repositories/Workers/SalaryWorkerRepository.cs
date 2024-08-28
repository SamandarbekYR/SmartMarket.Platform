using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class SalaryWorkerRepository : Repository<SalaryWorker>, ISalaryWorker
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<SalaryWorker> _salaryWorkers;

        public SalaryWorkerRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _salaryWorkers = appDb.Set<SalaryWorker>();
        }

        public async Task<List<SalaryWorker>> GetSalaryWorkersFullInformationAsync()
        {
            return await _salaryWorkers
                .Include(sw => sw.Worker) 
                .Include(sw => sw.Salary) 
                .ToListAsync();
        }
    }
}