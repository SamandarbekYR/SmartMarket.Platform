using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.PartnersCompany;
using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Repositories.PartnersCompany;

public class ContrAgentPaymentRepository : Repository<ContrAgentPayment>, IContrAgentPayment
{
    private readonly AppDbContext _appDbContext;
    private DbSet<ContrAgentPayment> _contrAgentPayments;

    public ContrAgentPaymentRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _contrAgentPayments = appDb.Set<ContrAgentPayment>();
    }

    public async Task<List<ContrAgentPayment>> GetContrAgentPaymentsFullInformationAsync()
    {
        return await _contrAgentPayments
            .Include(cap => cap.ContrAgent)
            .ToListAsync();
    }
}