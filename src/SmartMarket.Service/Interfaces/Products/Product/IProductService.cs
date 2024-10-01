using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Products.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Interfaces.Products.Product
{
    public interface IProductService
    {
        Task<Guid> AddAsync(AddProductDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<IEnumerable<ProductDto>> GetAllAsync(PaginationParams @params);
        Task<bool> UpdateAsync(AddProductDto dto, Guid Id);
        Task<bool> SellProductAsync(string barcode);
        Task<IEnumerable<ProductDto>> GetProductsByCategoryIdAsync(Guid categoryId, PaginationParams @params);

        Task<ProductDto> GetProductByBarcodeAsync(string barcode);
        Task<ProductDto> GetProductByPCodeAsync(string pCode);
        Task<ProductDto> GetProductByWorkerIdAsync(Guid workerId);
        Task<ProductDto> GetProductByNameAsync(string name);
    }
}
