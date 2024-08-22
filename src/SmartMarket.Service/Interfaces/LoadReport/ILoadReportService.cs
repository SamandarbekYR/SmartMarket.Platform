using SmartMarket.Service.DTOs.LoadReport;

namespace SmartMarket.Service.Interfaces.LoadReport;

public interface ILoadReportService
{
    Task<bool> AddAsync(AddLoadReportDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<LoadReportDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddLoadReportDto dto, Guid Id);
}
