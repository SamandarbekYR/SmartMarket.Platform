using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class DebtorRepository : Repository<Debtors>, IDebtors
    {
        public DebtorRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
