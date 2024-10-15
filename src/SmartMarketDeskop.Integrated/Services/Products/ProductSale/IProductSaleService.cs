using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductSale;

public interface IProductSaleService
{
    Task<List<ProductSaleViewModel>> GetAllAsync();
    Task<ProductSaleViewModel> GetByIdAsync(Guid id);
    Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto);
    Task<bool> UpdateAsync(AddProductSaleDto dto, Guid id);
    Task<bool> CreateAsync(AddProductSaleDto dto);
    Task<bool> DeleteAsync(Guid id);
}
