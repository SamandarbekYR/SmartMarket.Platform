using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers;

public class SalaryCheckRepository : Repository<SalaryCheck>, ISalaryCheck
{
    private readonly AppDbContext _appDbContext;
    private DbSet<SalaryCheck> _salaryChecks;

    public SalaryCheckRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _salaryChecks = appDb.Set<SalaryCheck>();
    }

    public async Task<List<SalaryCheck>> GetSalaryChecksFullInformationAsync()
    {
        return await _salaryChecks
            .Include(sc => sc.Worker) 
            .ToListAsync();
    }
}
