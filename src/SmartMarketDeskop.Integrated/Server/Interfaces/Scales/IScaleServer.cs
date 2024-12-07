using SmartMarketDesktop.DTOs.DTOs.Scales;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Scales
{
    public interface IScaleServer
    {
        Task<bool> AddAsync(AddScaleDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<List<ScaleDto>> GetAllAsync();
        Task<bool> UpdateAsync(AddScaleDto dto, Guid Id);
    }
}
