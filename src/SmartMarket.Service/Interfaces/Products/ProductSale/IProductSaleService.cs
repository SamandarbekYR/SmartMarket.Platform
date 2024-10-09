using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

namespace SmartMarket.Service.Interfaces.Products.ProductSale;

public interface IProductSaleService
{
    Task<bool> AddAsync(AddProductSaleDto dto);
    Task<bool> UpdateAsync(AddProductSaleDto dto, Guid Id);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<ProductSaleViewModel>> GetAllAsync();
    Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto);
    Task<List<ProductSaleDto>> GetProductSalesByTransactionAsync(Guid transactionId);
    Task<List<ProductSaleDto>> GetProductSalesByProductNameAsync(string productName);
}
