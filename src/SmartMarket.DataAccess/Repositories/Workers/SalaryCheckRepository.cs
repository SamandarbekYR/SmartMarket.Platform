using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class SalaryCheckRepository : Repository<SalaryCheck>, ISalaryCheck
    {
        public SalaryCheckRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
