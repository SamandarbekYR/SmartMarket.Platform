using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductSale
{
    public class ProductSaleService : IProductSaleService
    {
        private IProductSaleServer productSaleServer;

        public ProductSaleService()
        {
            this.productSaleServer = new ProductSaleServer();
        }

        public Task<List<ProductSaleViewModel>> FilterProductSaleAsync(FilterProductSaleDto dto)
        {
            if (IsInternetAvailable())
            {
                return productSaleServer.FilterProductSaleAsync(dto);
            }
            else
            {
                return Task.FromResult(new List<ProductSaleViewModel>());
            }
        }

        public async Task<List<ProductSaleViewModel>> GetAllAsync()
        {
            if (IsInternetAvailable())
            {
                var products = await productSaleServer.GetAllAsync();

                return products;
            }
            else
            {
                return new List<ProductSaleViewModel>();
            }
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
