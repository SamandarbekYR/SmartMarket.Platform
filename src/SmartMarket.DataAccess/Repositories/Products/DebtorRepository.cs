using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class DebtorRepository : Repository<Debtors>, IDebtors
{
    private readonly AppDbContext _appDbContext;
    private DbSet<Debtors> _debtors;

    public DebtorRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _debtors = appDb.Set<Debtors>();
    }

    public async Task<List<Debtors>> GetDebtorsFullInformationAsync()
    {
        return await _debtors
            .Include(d => d.Partner)
            .Include(d => d.Product)
            .Include(d => d.DebtPayment) 
            .ToListAsync();
    }
}
