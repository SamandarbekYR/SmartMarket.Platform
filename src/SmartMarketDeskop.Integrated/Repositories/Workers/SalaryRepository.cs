using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers;

public class SalaryRepository : Repository<SalaryView>, ISalary
{
    public SalaryRepository(AppDbContext appDb) : base(appDb)
    {
    }
}
