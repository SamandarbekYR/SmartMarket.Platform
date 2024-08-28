using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers;

public class WorkerDebtRepository : Repository<WorkerDebt>, IWorkerDebt
{
    private readonly AppDbContext _appDbContext;
    private DbSet<WorkerDebt> _workerDebts;

    public WorkerDebtRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _workerDebts = appDb.Set<WorkerDebt>();
    }

    public async Task<List<WorkerDebt>> GetWorkerDebtsFullInformationAsync()
    {
        return await _workerDebts
            .Include(wd => wd.Worker)
            .ToListAsync();
    }
}
