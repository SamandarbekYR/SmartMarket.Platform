using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.PartnersCompany;
using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Repositories.PartnersCompany;

public class ContrAgentRepository : Repository<ContrAgent>, IContrAgent
{
    private readonly AppDbContext _appDbContext;
    private DbSet<ContrAgent> _contrAgents;

    public ContrAgentRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _contrAgents = appDb.Set<ContrAgent>();
    }

    public IQueryable<ContrAgent> GetContrAgentsFullInformation()
    {
        return _contrAgents
            .Include(c => c.PartnerCompany)
            .Include(c => c.Products)
            .Include(c => c.LoadReports)
            .Include(c => c.ContrAgentPayment)
                .ThenInclude(ca => ca.PayDesk)
            .AsQueryable();
    }

    public async Task<List<ContrAgent>> GetContrAgentsFullInformationAsync()
    {
        return await _contrAgents
            .Include(ca => ca.PartnerCompany) 
            .Include(ca => ca.Products)  
            .Include(ca => ca.LoadReports) 
            .Include(ca => ca.ContrAgentPayment) 
            .ToListAsync();
    }
}
