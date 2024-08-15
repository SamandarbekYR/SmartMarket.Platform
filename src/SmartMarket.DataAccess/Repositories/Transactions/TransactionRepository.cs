using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Transactions;
using SmartMarket.Domain.Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Repositories.Transactions
{
    public class TransactionRepository : Repository<Transaction>, ITransaction
    {
        public TransactionRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
