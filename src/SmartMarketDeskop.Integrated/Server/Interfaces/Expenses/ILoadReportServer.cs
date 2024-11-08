using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Expenses
{
    public interface ILoadReportServer
    {
        Task<List<LoadReportDto>> GetAllAsync();
        Task<List<LoadReportDto>> FilterLoadReportAsync(FilterLoadReportDto dto);
        Task<List<LoadReportDto>> GetLoadReportsByContrAgentIdAsync(Guid contrAgentId);
        Task<LoadReportStatisticsDto> GetLoadReportStatisticsAsync();
        Task<List<CollectedLoadReportDto>> GetAllCollectedAsync();
        Task<List<CollectedLoadReportDto>> FilterCollectedLoadReportAsync(FilterLoadReportDto dto);
    }
}
