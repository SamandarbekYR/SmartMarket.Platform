using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class DebtorRepository : Repository<DebttorsView>, IDebtors
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<DebttorsView> _debtors;

        public DebtorRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _debtors = appDb.Set<DebttorsView>();
        }

        public async Task<List<DebttorsView>> GetDebtorsFullInformationAsync()
        {
            return await _debtors
                .Include(d => d.PartnerView)
                .Include(d => d.ProductView)
                .Include(d => d.DebtPaymentViews)
                .ToListAsync();
        }
    }
}
