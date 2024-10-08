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
