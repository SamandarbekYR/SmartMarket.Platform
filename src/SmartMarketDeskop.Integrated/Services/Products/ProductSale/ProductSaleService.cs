using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.ViewModels.Products;

using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;

using SmartMarketDesktop.ViewModels.Entities.Products;

using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.ProductSale
{
    public class ProductSaleService : IProductSaleService
    {
        private IProductSaleServer productSaleServer;
        private IWorkerServer workerServer;

        public ProductSaleService()
        {
            this.productSaleServer = new ProductSaleServer();
            this.workerServer = new WorkerServer();
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

        public Task<List<ProductSaleView>> GetByDateTime(DateTime formDateTime, DateTime toDateTime)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSaleView>> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductSaleView>> GetBySellerName(string name)
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
