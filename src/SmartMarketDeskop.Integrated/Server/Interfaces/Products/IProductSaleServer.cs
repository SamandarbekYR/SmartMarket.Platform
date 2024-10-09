using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products
{
    public interface IProductSaleServer
    {
        Task<List<ProductSaleViewModel>> GetAllAsync();
        Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto);
    }
}
