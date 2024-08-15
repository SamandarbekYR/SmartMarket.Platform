using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class SalaryRepository : Repository<Salary>, ISalary
    {
        public SalaryRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
