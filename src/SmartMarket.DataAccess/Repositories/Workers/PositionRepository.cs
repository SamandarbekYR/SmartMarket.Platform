using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Workers
{
    public class PositionRepository : Repository<Position>, IPosition
    {
        public PositionRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
