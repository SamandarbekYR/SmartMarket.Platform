using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Scales;

using SmartMarketDesktop.ViewModels.Entities.Scales;

namespace SmartMarketDeskop.Integrated.Repositories.Scales
{
    public class ScaleRepository : Repository<ScaleView>, IScale
    {
        public ScaleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
