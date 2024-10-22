using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface ISalesRequest : IRepository<SalesRequest>
    {
        public Task<List<SalesRequest>> GetSalesRequestsFullInformationAsync();
    }
}
