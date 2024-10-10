using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;

using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;

using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.InvalidProduct
{
    public class InvalidProductService : IInvalidProductService
    {
        private readonly IInvalidProductServer _invalidProductServer;

        public InvalidProductService()
        {
            _invalidProductServer = new InvalidProductServer();
        }
        public async Task<bool> AddAsync(AddReplaceProductDto dto)
        {
            if(IsInternetAvailable())
            {
                return await _invalidProductServer.AddAsync(dto);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            if(IsInternetAvailable())
            {
                return await _invalidProductServer.DeleteAsync(Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<InvalidProductDto>> GetAllAsync()
        {
            if(IsInternetAvailable())
            {
                return await _invalidProductServer.GetAllAsync();
            }
            else
            {
                return new List<InvalidProductDto>();
            }
        }

        public async Task<bool> UpdateAsync(AddInvalidProductDto dto, Guid Id)
        {
            if(IsInternetAvailable())
            {
                return await _invalidProductServer.UpdateAsync(dto, Id);
            }
            else
            {
                return false;
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
