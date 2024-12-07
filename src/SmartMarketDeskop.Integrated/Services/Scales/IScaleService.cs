using SmartMarketDesktop.DTOs.DTOs.Scales;

namespace SmartMarketDeskop.Integrated.Services.Scales
{
    public interface IScaleService
    {
        Task<bool> CreateScaleAsync(AddScaleDto dto);
        Task<bool> DeleteScaleAsync(Guid Id);
        Task<List<ScaleDto>> GetAllScalesAsync();
        Task<bool> UpdateScaleAsync(AddScaleDto dto, Guid Id);
    }
}
