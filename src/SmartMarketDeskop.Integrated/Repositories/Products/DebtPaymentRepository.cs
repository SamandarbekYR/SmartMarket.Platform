using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class DebtPaymentRepository : Repository<DebtPaymentView>, IDebtPayment
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<DebtPaymentView> _debtPayments;

        public DebtPaymentRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _debtPayments = appDb.Set<DebtPaymentView>();
        }

        public async Task<List<DebtPaymentView>> GetDebtPaymentsFullInformationAsync()
        {
            return await _debtPayments
                .Include(dp => dp.DebttorsView) 
                .ToListAsync();
        }
    }
}