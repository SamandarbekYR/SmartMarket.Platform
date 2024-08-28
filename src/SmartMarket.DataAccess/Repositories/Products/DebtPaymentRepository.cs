using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class DebtPaymentRepository : Repository<DebtPayment>, IDebtPayment
{
    private readonly AppDbContext _appDbContext;
    private DbSet<DebtPayment> _debtPayments;

    public DebtPaymentRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _debtPayments = appDb.Set<DebtPayment>();
    }

    public async Task<List<DebtPayment>> GetDebtPaymentsFullInformationAsync()
    {
        return await _debtPayments
            .Include(dp => dp.Debtor)
            .ToListAsync();
    }
}
