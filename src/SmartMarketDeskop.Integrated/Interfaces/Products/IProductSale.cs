using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Interfaces.Products;

public interface IProductSale : IRepository<ProductSaleView>
{
    public Task<List<ProductSaleView>> GetProductSalesFullInformationAsync();
}
