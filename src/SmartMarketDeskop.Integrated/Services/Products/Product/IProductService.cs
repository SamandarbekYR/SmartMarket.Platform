using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public interface IProductService
{
    Task<bool> CreateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto dto);
    Task<bool> UpdateProduct(SmartMarketDesktop.DTOs.DTOs.Product.AddProductDto product, Guid Id);
    Task<bool> DeleteProduct(Guid Id);
    Task<List<FullProductDto>> GetAll();
    Task<List<FullProductDto>> GetByCategoryId(Guid categoryId);
    Task<FullProductDto> GetByBarCode(string barCode);
    Task<List<FullProductDto>> GetByPCode(string PCode);
    Task<List<FullProductDto>> GetByProductName(string productName);
    Task<List<FullProductDto>> GetFinishedProducts();
    Task<bool> UpdateProductCountAsync(List<UpdateProductDto> dto);
 }
