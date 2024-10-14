using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface ILoadReport : IRepository<LoadReport>
    {
       public IQueryable<LoadReport> GetLoadReportsFullInformationAsync();
    }
}
