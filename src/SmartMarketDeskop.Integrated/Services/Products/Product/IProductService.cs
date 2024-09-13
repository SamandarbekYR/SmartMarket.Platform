using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.DTOs.DTOs.Product;

namespace SmartMarketDeskop.Integrated.Services.Products.Product;

public interface IProductService
{
    Task<bool> CreateProduct(AddProductDto dto);
    Task<bool> UpdateProduct(AddProductDto product,Guid Id);
    Task<bool> DeleteProduct(Guid Id);
    Task<List<ProductViewModels>> GetAll();
 }
