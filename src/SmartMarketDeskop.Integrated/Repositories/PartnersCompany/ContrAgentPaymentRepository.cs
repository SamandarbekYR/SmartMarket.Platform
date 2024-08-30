using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.PartnersCompany;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.PartnersCompany
{
    public class ContrAgentPaymentRepository : Repository<ContrAgentPaymentView>, IContrAgentPayment
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ContrAgentPaymentView> _contrAgentPayments;

        public ContrAgentPaymentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
            _contrAgentPayments = appDbContext.Set<ContrAgentPaymentView>();
        }

        public async Task<List<ContrAgentPaymentView>> GetContrAgentPaymentsFullInformationAsync()
        {
            return await _contrAgentPayments
                .Include(cap => cap.ContrAgentView)
                .ToListAsync();
        }
    }
}