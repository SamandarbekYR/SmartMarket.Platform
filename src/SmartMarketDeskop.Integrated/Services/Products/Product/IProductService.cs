using SmartMarketDesktop.DTOs.DTOs.Product;
using SmartMarket.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;

namespace SmartMarketDeskop.Integrated.Services.Products.Product
{
    public interface IProductService
    {
        Task<bool> CreateProduct(AddProductDto dto);
        Task<bool> UpdateProduct(AddProductDto product,Guid Id);
        Task<bool> DeleteProduct(Guid Id);
        Task<List<ProductViewModels>> GetAll();
     }
}
