using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface IReplaceProduct : IRepository<ReplaceProductView>
{
    public Task<List<ReplaceProductView>> GetReplaceProductsFullInformationAsync();
}
