using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Partners;
using SmartMarket.Domain.Entities.Partners;

namespace SmartMarket.DataAccess.Repositories.Partners
{
    public class PartnerRepository : Repository<Partner>, IPartner
    {
        public PartnerRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
