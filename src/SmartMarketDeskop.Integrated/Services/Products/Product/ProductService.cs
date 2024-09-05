using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.DTOs.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Products.Product
{
    public class ProductService : IProductService
    {
        private IProductServer productServer;

        public ProductService()
        {
            this.productServer = new ProductServer();
        }

        public async Task<bool> CreateProduct(AddProductDto dto)
        {
            if(IsInternetAvailable())
            {
                await productServer.AddAsync(dto);
                return true;    
            }
            else
            {
                // local baza uchun malumot saqlanadi
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

        public Task<bool> UpdateProduct(AddProductDto product, Guid Id)
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
