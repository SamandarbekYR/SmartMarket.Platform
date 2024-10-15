using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDeskop.Integrated.Server.Repositories.Workers;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Position
{
    public class PositionService : IPositionService
    {
        private IPositionServer _positionServer;

        public PositionService()
        {
            _positionServer = new PositionServer();
        }

        public async Task<bool> AddAsync(AddPositionDto dto)
        {
            if (IsInternetAvailable())
            {
                return await _positionServer.AddAsync(dto);
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
                return await _positionServer.DeleteAsync(Id);
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PositionDto>> GetAllAsync()
        {
            if (IsInternetAvailable())
            {
                return await _positionServer.GetAllAsync();
            }
            else
            {
                return new List<PositionDto>();
            }
        }

        public async Task<bool> UpdateAsync(AddPositionDto positionDto, Guid Id)
        {
            if (IsInternetAvailable())
            {
                return await _positionServer.UpdateAsync(positionDto, Id);
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
