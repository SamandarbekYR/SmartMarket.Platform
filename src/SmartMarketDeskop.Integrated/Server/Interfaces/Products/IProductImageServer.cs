using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.DTOs.Products.ProductImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products
{
    public interface IProductImageServer
    {
        Task<List<ProductImage>> GetAllAsync();
        Task<bool> AddAsync(ProductImageDto dto);

        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(ProductImageDto dto, Guid Id);
    }
}
