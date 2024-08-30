using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface IReplaceProduct : IRepository<ReplaceProductView>
{
    public Task<List<ReplaceProductView>> GetReplaceProductsFullInformationAsync();
}
