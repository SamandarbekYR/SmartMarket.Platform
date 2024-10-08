using SmartMarket.Service.DTOs.Products.ProductSale;

namespace SmartMarket.Service.Interfaces.Products.ProductSale;

public interface IProductSaleService
{
    Task<bool> AddAsync(AddProductSaleDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ProductSaleDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id);

    Task<List<ProductSaleDto>> GetProductSalesByTransactionAsync(Guid transactionId);
    Task<List<ProductSaleDto>> GetProductSalesByProductNameAsync(string productName);
}
