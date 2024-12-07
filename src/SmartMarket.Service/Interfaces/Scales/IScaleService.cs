using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Scales;

namespace SmartMarket.Service.Interfaces.Scales
{
    public interface IScaleService
    {
        Task<bool> AddAsync(AddScaleDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<List<ScaleDto>> GetAllAsync();
        Task<bool> UpdateAsync(AddScaleDto dto, Guid Id);
    }
}
