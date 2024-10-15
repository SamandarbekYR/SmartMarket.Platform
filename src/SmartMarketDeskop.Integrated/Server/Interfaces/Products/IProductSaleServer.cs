using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductSaleServer
{
    Task<List<ProductSaleViewModel>> GetAllAsync();
    Task<ProductSaleViewModel> GetByIdAsync(Guid id);
    Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto);
    Task<bool> AddAsync(AddProductSaleDto dto);
    Task<bool> UpdateAsync(AddProductSaleDto dto, Guid id);
    Task<bool> DeleteAsync(Guid id);
}
