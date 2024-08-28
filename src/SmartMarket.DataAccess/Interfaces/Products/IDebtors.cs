using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IDebtors : IRepository<Debtors>
    {
        public Task<List<Debtors>> GetDebtorsFullInformationAsync();
    }
}
