using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface IInvalidProduct : IRepository<InValidProductView>
{
    public Task<List<InValidProductView>> GetInvalidProductsFullInformationAsync();
}
