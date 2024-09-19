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


        Task<ProductDto> GetProductByBarcodeAsync(string barcode);
        Task<ProductDto> GetProductByPCodeAsync(string pCode);
        Task<ProductDto> GetProductByWorkerIdAsync(Guid workerId);
        Task<IEnumerable<ProductDto>> SearchProductsAsync(string searchTerm, PaginationParams @params);
    }
}