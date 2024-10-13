using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductServer
{
    Task<List<FullProductDto>> GetAllAsync();
    Task<List<FullProductDto>> GetByCategoryIdAsync(Guid categoryId);
    Task<bool> AddAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto, Guid Id);
    Task<ProductDto> GetByBarCodeAsync(string barcode);
    Task<ProductDto> GetByPCodeAsync(string PCode);
    Task<List<ProductDto>> GetFinishedProductsAsync();
}

