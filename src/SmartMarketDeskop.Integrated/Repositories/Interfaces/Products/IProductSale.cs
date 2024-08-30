using SmartMarketDeskop.Integrated.Repositories.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Interfaces.Products;

public interface IProductSale : IRepository<ProductSaleView>
{
    public Task<List<ProductSaleView>> GetProductSalesFullInformationAsync();
}
