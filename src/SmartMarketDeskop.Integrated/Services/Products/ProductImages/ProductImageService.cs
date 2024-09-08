using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductImages
{
    public class ProductImageService : IProductImageService
    {
        private IProductImageServer productImageServer;
        public ProductImageService()
        {
            this.productImageServer=new ProductImageServer();
        }


        public async Task<bool> CreateProduct(ProductImageDto dto)
        {
            if(IsInternetAvailable())
            {
                await productImageServer.AddAsync(dto);
                return true;    
            }
            else
            {
                // local bazaga saqlanadi
                return false;
            }
        }

        public Task<bool> DeleteProduct(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModels>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProduct(ProductImageDto image, Guid Id)
        {
            throw new NotImplementedException();
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient()!)
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
