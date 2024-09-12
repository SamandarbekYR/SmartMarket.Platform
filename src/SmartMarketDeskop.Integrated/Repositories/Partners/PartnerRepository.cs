using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Partners;
using SmartMarketDesktop.ViewModels.Entities.Partners;

namespace SmartMarketDeskop.Integrated.Repositories.Partners
{
    public class PartnerRepository : Repository<PartnerView>, IPartner
    {
        public PartnerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}