using SmartMarket.Domain.Entities.Products;
using SmartMarketDesktop.DTOs.DTOs.Product;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Products;

public interface IProductServer
{
    Task<List<Product>> GetAllAsync();
    Task<bool> AddAsync(AddProductDto dto);

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(AddProductDto dto, Guid Id);
}
