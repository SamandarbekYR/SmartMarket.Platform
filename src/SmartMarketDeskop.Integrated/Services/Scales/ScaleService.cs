using SmartMarket.Service.DTOs.PayDesks;

using SmartMarketDeskop.Integrated.Server.Interfaces.Scales;
using SmartMarketDeskop.Integrated.Server.Repositories.Scales;

using SmartMarketDesktop.DTOs.DTOs.Scales;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Scales
{
    public class ScaleService : IScaleService
    {
        private readonly IScaleServer _scaleServer;

        public ScaleService()
        {
            _scaleServer = new ScaleServer();
        }

        public async Task<bool> CreateScaleAsync(AddScaleDto dto)
        {
            if (IsInternetAvailable())
            {
                bool result = await _scaleServer.AddAsync(dto);

                if (result)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteScaleAsync(Guid Id)
        {
            if (IsInternetAvailable())
            {
                bool result = await _scaleServer.DeleteAsync(Id);

                if (result)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<ScaleDto>> GetAllScalesAsync()
        {
            if (IsInternetAvailable())
            {
                var paydesk = await _scaleServer.GetAllAsync();
                return paydesk;
            }
            else
            {
                return new List<ScaleDto>();
            }
        }

        public async Task<bool> UpdateScaleAsync(AddScaleDto dto, Guid Id)
        {
            if (IsInternetAvailable())
            {
                bool result = await _scaleServer.UpdateAsync(dto, Id);

                if (result)
                    return true;
                else
                    return false;
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
