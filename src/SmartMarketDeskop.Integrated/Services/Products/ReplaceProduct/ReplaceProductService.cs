using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDeskop.Integrated.Server.Repositories.Products;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Products.ReplaceProduct
{
    public class ReplaceProductService : IReplaceProductService
    {
        private readonly IReplaceProductServer replaceProductServer;

        public ReplaceProductService()
        {
            replaceProductServer = new ReplaceProductServer();
        }

        public async Task<bool> AddAsync(AddReplaceProductDto dto)
        {
            if (IsInternetAvailable())
            {
                return await replaceProductServer.AddAsync(dto);
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            if (IsInternetAvailable())
            {
                return await replaceProductServer.DeleteAsync(Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ReplaceProductDto>> GetAllAsync()
        {
            if (IsInternetAvailable())
            {
                return await replaceProductServer.GetAllAsync();
            }
            else
            {
                return new List<ReplaceProductDto>();
            }
        }

        public async Task<bool> UpdateAsync(AddReplaceProductDto dto, Guid Id)
        {
            if (IsInternetAvailable())
            {
                return await replaceProductServer.UpdateAsync(dto, Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ReplaceProductDto>> FilterReplaceProductAsync(FilterReplaceProductDto dto)
        {
            if (IsInternetAvailable())
            {
                return await replaceProductServer.FilterReplaceProductAsync(dto);
            }
            else
            {
                return new List<ReplaceProductDto>();
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
