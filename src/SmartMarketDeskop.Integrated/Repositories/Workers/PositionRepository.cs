using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;

namespace SmartMarketDeskop.Integrated.Repositories.Workers;

public class PositionRepository : Repository<PositionView>, IPosition
{
    public PositionRepository(AppDbContext appDb) : base(appDb)
    {
    }
}
