using SmartMarket.Service.DTOs.Products.Product;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public interface IProductService
{
    Task<bool> CreateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> UpdateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto product, Guid Id);
    Task<bool> DeleteProduct(Guid Id);
    Task<List<FullProductDto>> GetAll();
    Task<List<FullProductDto>> GetByCategoryId(Guid categoryId);
    Task<ProductDto> GetByBarCode(string barCode);
 }
