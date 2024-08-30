using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface IDebtPayment : IRepository<DebtPaymentView>
{
    public Task<List<DebtPaymentView>> GetDebtPaymentsFullInformationAsync();
}
