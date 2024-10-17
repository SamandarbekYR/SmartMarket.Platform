using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using SmartMarketDeskop.Integrated.Server.Repositories.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
