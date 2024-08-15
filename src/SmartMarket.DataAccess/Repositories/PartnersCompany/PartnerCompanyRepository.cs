using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.PartnersCompany;
using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Repositories.PartnersCompany
{
    public class PartnerCompanyRepository : Repository<PartnerCompany>, IPartnerCompany
    {
        public PartnerCompanyRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
