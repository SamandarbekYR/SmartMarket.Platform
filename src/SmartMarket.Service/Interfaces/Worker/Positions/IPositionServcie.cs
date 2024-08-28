using SmartMarket.Service.DTOs.Workers.Position;

namespace SmartMarket.Service.Interfaces.Worker.Positions;

public interface IPositionService
{
    Task<bool> AddAsync(AddPositionDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PositionDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddPositionDto dto, Guid Id);
}