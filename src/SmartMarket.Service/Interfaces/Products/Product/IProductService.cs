using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Interfaces.Products.Product
{
    public interface IProductService
    {
        Task<Guid> AddAsync(AddProductDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<IEnumerable<Et.Product>> GetAllAsync(PaginationParams @params);
        Task<bool> UpdateAsync(AddProductDto dto, Guid Id);
        Task<bool> SellProductAsync(string barcode);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, PaginationParams @params);

        Task<ProductDto> GetProductByBarcodeAsync(string barcode);
        Task<ProductDto> GetProductByPCodeAsync(string pCode);
        Task<ProductDto> GetProductByWorkerIdAsync(Guid workerId);
        Task<ProductDto> GetProductByNameAsync(string name);
        Task<IEnumerable<Et.Product>> GetProductsFullInformationAsync(PaginationParams @params);
        Task<IEnumerable<ProductDto>> GetFinishedProductsAsync(PaginationParams @params);
    }
}
