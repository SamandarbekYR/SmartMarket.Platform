using SmartMarket.Service.DTOs.ProductSale;

namespace SmartMarket.Service.Interfaces.ProductSale;

public interface IProductSaleService
{
    Task<bool> AddAsync(AddProductSaleDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ProductSaleDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id);
}
