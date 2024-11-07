using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using System.CodeDom.Compiler;
using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Expenses
{
    public class LoadReportService : ILoadReportService
    {
        private readonly ILoadReportServer _loadReportServer;
        public LoadReportService()
        {
            this._loadReportServer = new LoadReportServer();
        }

        public async Task<List<LoadReportDto>> FilterAsync(FilterLoadReportDto dto)
        {
            if(IsInternetAvialable())
            {
                var loadReports = await _loadReportServer.FilterLoadReportAsync(dto);
                return loadReports;
            }
            else { return new List<LoadReportDto>(); }
        }

        public async Task<List<LoadReportDto>> GetAll()
        {
            if(IsInternetAvialable())
            {
                var loadReports = await _loadReportServer.GetAllAsync();
                return loadReports;
            }
            else { return new List<LoadReportDto>(); }
        }

        public async Task<List<CollectedLoadReportDto>> GetAllCollected()
        {
            if(IsInternetAvialable())
            {
                var loadReports = await _loadReportServer.GetAllCollectedAsync();
                return loadReports;
            }
            else { return new List<CollectedLoadReportDto>(); }
        }

        public async Task<List<LoadReportDto>> GetByContrAgentIdAsync(Guid contrAgentId)
        {
            if(IsInternetAvialable())
            {
                var loadReports = await _loadReportServer.GetLoadReportsByContrAgentIdAsync(contrAgentId);
                return loadReports;
            }
            else { return new List<LoadReportDto>(); }
        }

        public async Task<LoadReportStatisticsDto> GetStatisticsAsync()
        {
            if(IsInternetAvialable())
            {
                var loadReportStatistics = await _loadReportServer.GetLoadReportStatisticsAsync();
                return loadReportStatistics;
            }
            else { return new LoadReportStatisticsDto(); }
        }

        public bool IsInternetAvialable()
        {
            try
            {
                using (var client = new WebClient()!)
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
