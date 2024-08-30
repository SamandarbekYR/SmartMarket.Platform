using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.PartnersCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

namespace SmartMarketDeskop.Integrated.Repositories.PartnersCompany
{
    public class PartnerCompanyRepository : Repository<PartnerCompanyView>, IPartnerCompany
    {
        public PartnerCompanyRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
