using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarket.Service.DTOs.Workers.Worker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Workers
{
    public interface IPositionServer
    {
        Task<List<PositionDto>> GetAllAsync();
        Task<bool> AddAsync(AddPositionDto dto);

        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(AddPositionDto dto, Guid Id);
    }
}
