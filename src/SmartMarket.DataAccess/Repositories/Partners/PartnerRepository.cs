using Microsoft.EntityFrameworkCore;

using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Partners;
using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Partners
{
    public class PartnerRepository : Repository<Partner>, IPartner
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<Partner> _partners;
        public PartnerRepository(AppDbContext appDb) : base(appDb)
        { 
            _appDbContext = appDb;
            _partners = appDb.Set<Partner>();
        }

        public async Task<List<Partner>> GetPartnersFullInformationAsync()
        {
            return await _partners
                .Include(p => p.Debtors)
                    .ThenInclude(d => d.DebtPayment)
                .ToListAsync();
        }
    }
}
