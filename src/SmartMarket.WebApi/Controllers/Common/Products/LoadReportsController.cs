using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.Interfaces.Products.LoadReport;

namespace SmartMarket.WebApi.Controllers.Common.Products;

[Route("api/load-reports")]
[ApiController]
public class LoadReportsController(ILoadReportService loadReportService) : ControllerBase
{
    private readonly ILoadReportService _loadReportService = loadReportService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var loadReports = await _loadReportService.GetAllAsync();
        return Ok(loadReports);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddLoadReportDto dto)
    {
        await _loadReportService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _loadReportService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddLoadReportDto dto)
    {
        await _loadReportService.UpdateAsync(dto, id);
        return Ok();
    }
}
