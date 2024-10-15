using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.ViewModelsForUI.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Workers.Position
{
    public interface IPositionService
    {
        Task<bool> AddAsync(AddPositionDto dto);
        Task<bool> UpdateAsync(AddPositionDto positionDto, Guid Id);
        Task<bool> DeleteAsync(Guid Id);
        Task<List<PositionDto>> GetAllAsync();
    }
}
