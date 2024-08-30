using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface IDebtPayment : IRepository<DebtPaymentView>
{
    public Task<List<DebtPaymentView>> GetDebtPaymentsFullInformationAsync();
}
