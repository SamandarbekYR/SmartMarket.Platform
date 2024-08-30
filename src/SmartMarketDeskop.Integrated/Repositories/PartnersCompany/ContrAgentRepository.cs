using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Repositories.Interfaces.PartnersCompany;
using SmartMarketDesktop.ViewModels.Entities.Orders;
using SmartMarketDesktop.ViewModels.Entities.PartnersCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.PartnersCompany
{
    public class ContrAgentRepository : Repository<ContrAgentView>, IContrAgent
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ContrAgentView> _contrAgents; 

        public ContrAgentRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _contrAgents = appDb.Set<ContrAgentView>(); 
        }

        public async Task<List<ContrAgentView>> GetContrAgentsFullInformationAsync()
        {
            return await _contrAgents 
                .Include(ca => ca.PartnerCompanyView)
                .Include(ca => ca.ProductViews)
                .Include(ca => ca.LoadReportViews)
                .Include(ca => ca.ContrAgentPaymentViews)
                .ToListAsync();
        }
    }
}
