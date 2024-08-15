using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.PartnersCompany;
using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Repositories.PartnersCompany
{
    public class ContrAgentRepository : Repository<ContrAgent>, IContrAgent
    {
        public ContrAgentRepository(AppDbContext appDb) : base(appDb)
        { }
    }
}
