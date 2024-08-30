using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers;

public class WorkerRoleRepository : Repository<WorkerRoleView>, IWorkerRole
{
    public WorkerRoleRepository(AppDbContext appDb) : base(appDb)
    {
    }
}
