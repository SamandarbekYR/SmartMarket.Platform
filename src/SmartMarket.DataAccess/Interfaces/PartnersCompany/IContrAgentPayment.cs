using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Interfaces.PartnersCompany;

public interface IContrAgentPayment : IRepository<ContrAgentPayment>
{
    public Task<List<ContrAgentPayment>> GetContrAgentPaymentsFullInformationAsync();
    public IQueryable<ContrAgentPayment> GetContrAgentPaymentsFullInformation();
}
