using SmartMarket.Service.DTOs.Products.LoadReport;

namespace SmartMarket.Service.Interfaces.Products.LoadReport;

public interface ILoadReportService
{
    Task<bool> AddAsync(AddLoadReportDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<LoadReportDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddLoadReportDto dto, Guid Id);

    Task<List<LoadReportDto>> GetLoadReportsByCompanyNameAsync(string companyName);
}
