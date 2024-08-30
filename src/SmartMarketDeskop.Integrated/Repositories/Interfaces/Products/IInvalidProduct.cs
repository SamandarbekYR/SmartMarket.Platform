using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface IInvalidProduct : IRepository<InValidProductView>
{
    public Task<List<InValidProductView>> GetInvalidProductsFullInformationAsync();
}
