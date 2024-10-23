using Et = SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Interfaces.Products.Product
{
    public interface IProductService
    {
        Task<Guid> AddAsync(AddProductDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<IList<FullProductDto>> GetAllAsync(PaginationParams @params);
        Task<FullProductDto> GetByIdAsync(Guid Id);
        Task<bool> UpdateAsync(AddProductDto dto, Guid Id);
        Task<bool> SellProductAsync(string barcode);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, PaginationParams @params);

        Task<IList<FullProductDto>> GetProductByBarcodeAsync(string barcode);
        Task<IList<FullProductDto>> GetProductByPCodeAsync(string pCode);
        Task<IList<FullProductDto>> GetProductByWorkerIdAsync(Guid workerId);
        Task<IList<FullProductDto>> GetProductsByNameAsync(string name);
        Task<IEnumerable<Et.Product>> GetProductsFullInformationAsync(PaginationParams @params);
        Task<IEnumerable<ProductDto>> GetFinishedProductsAsync(PaginationParams @params);
    }
}
