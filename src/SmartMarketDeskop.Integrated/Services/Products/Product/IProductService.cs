using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public interface IProductService
{
    Task<bool> CreateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> UpdateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto product, Guid Id);
    Task<bool> DeleteProduct(Guid Id);
    Task<List<ProductDto>> GetAll();
    Task<List<ProductDto>> GetByCategoryId(Guid categoryId);
    Task<ProductDto> GetByBarCode(string barCode);
    Task<ProductDto> GetByPCode(string PCode);
    Task<ProductDto> GetByProductName(string productName);
 }
