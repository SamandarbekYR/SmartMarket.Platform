using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductServer
{
    Task<List<ProductDto>> GetAllAsync();
    Task<List<ProductDto>> GetByCategoryIdAsync(Guid categoryId);
    Task<bool> AddAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto, Guid Id);
    Task<ProductDto> GetByBarCodeAsync(string barcode);
    Task<ProductDto> GetByPCodeAsync(string PCode);    
    Task<ProductDto> GetByProductNameAsync(string productName);
    Task<List<ProductDto>> GetFinishedProductsAsync();
}

