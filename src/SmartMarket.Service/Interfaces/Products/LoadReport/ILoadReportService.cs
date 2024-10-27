using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Products.LoadReport;

namespace SmartMarket.Service.Interfaces.Products.LoadReport;

public interface ILoadReportService
{
    Task<bool> AddAsync(AddLoadReportDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<LoadReportDto>> GetAllAsync();
    Task<LoadReportDto> GetByIdAsync(Guid Id);
    Task<bool> UpdateAsync(AddLoadReportDto dto, Guid Id);
    Task<IEnumerable<LoadReportDto>> GetLoadReportsByContrAgentIdAsync(Guid contrAgentId);
    Task<List<LoadReportDto>> GetLoadReportsByCompanyNameAsync(string companyName);
    Task<List<LoadReportDto>> FilterLoadReportAsync(FilterLoadReportDto dto);
    Task<LoadReportStatisticsDto> GetStatisticsAsync();
}
