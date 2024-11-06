using SmartMarket.Service.DTOs.Products.LoadReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Expenses
{
    public interface ILoadReportService
    {
        Task<List<LoadReportDto>> GetAll();
        Task<List<LoadReportDto>> FilterAsync(FilterLoadReportDto dto);
        Task<List<LoadReportDto>> GetByContrAgentIdAsync(Guid contrAgentId);
        Task<LoadReportStatisticsDto> GetStatisticsAsync();
        Task<List<CollectedLoadReportDto>> GetAllCollected();
    }
}
