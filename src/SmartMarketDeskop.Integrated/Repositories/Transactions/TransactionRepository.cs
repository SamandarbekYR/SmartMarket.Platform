using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Transactions;
using SmartMarketDesktop.ViewModels.Entities.Transactions;

namespace SmartMarketDeskop.Integrated.Repositories.Transactions;

public class TransactionRepository : Repository<TransactionView>, ITransaction
{
    public TransactionRepository(AppDbContext appDb) : base(appDb)
    {
    }
}
