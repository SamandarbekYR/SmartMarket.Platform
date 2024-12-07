using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Scales;
using SmartMarket.Domain.Entities.Scales;

namespace SmartMarket.DataAccess.Repositories.Scales
{
    public class ScaleRepository : Repository<Scale>, IScale
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<Scale> _scales;

        public ScaleRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _scales = appDb.Set<Scale>();
        }
    }
}
