using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Products.Product
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductView productView);
        Task<bool> UpdateProduct(ProductView productView);
        Task<bool> DeleteProduct(Guid Id);
        Task<List<ProductView>> GetAllProduct();
    }
}
