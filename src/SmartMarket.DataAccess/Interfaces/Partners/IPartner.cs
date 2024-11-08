using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Interfaces.Partners
{
    public interface IPartner : IRepository<Partner>
    {
        public Task<List<Partner>> GetPartnersFullInformationAsync();
    }
}
