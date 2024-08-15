using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products
{
    public class DebtPaymentRepository : Repository<DebtPayment>, IDebtPayment
    {
        public DebtPaymentRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
