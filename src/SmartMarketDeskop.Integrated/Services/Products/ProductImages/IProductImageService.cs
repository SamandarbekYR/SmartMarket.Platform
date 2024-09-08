using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductImages
{
    public interface IProductImageService
    {
        Task<bool> CreateProduct(ProductImageDto dto);
        Task<bool> UpdateProduct(ProductImageDto image, Guid Id);
        Task<bool> DeleteProduct(Guid Id);
        Task<List<ProductViewModels>> GetAll();
    }
}
