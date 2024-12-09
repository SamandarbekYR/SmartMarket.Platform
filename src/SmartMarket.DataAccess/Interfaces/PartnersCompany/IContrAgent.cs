using SmartMarket.Domain.Entities.PartnersCompany;

namespace SmartMarket.DataAccess.Interfaces.PartnersCompany
{
    public interface IContrAgent : IRepository<ContrAgent>
    {
        public  Task<List<ContrAgent>> GetContrAgentsFullInformationAsync();
        public IQueryable<ContrAgent> GetContrAgentsFullInformation();
    }
}
