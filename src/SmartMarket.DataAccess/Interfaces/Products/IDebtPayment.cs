using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Interfaces.Products
{
    public interface IDebtPayment : IRepository<DebtPayment>
    {
        public Task<List<DebtPayment>> GetDebtPaymentsFullInformationAsync();
    }
}
