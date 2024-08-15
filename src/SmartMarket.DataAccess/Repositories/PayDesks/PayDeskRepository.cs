using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.PayDesks;
using SmartMarket.Domain.Entities.PayDesks;

namespace SmartMarket.DataAccess.Repositories.PayDesks
{
    public class PayDeskRepository : Repository<PayDesk>, IPayDesk
    {
        public PayDeskRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
