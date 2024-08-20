using SmartMarket.Service.DTOs.Position;

namespace SmartMarket.Service.Interfaces.Positions;

public interface IPositionService
{
    Task<bool> AddAsync(AddPositionDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<PositionDto>> GetAllAsync();
    Task<bool> UpdateAsync(PositionDto dto, Guid Id);
}