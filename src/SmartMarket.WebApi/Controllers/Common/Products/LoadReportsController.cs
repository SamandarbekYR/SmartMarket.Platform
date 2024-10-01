using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.Interfaces.Products.LoadReport;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/load-reports")]
    [ApiController]
    public class LoadReportsController(ILoadReportService loadReportService) : ControllerBase
    {
        private readonly ILoadReportService _loadReportService = loadReportService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var loadReports = await _loadReportService.GetAllAsync();
                return Ok(loadReports);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); 
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddLoadReportDto dto)
        {
            try
            {
                await _loadReportService.AddAsync(dto);
                return Ok();
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _loadReportService.DeleteAsync(id);
                return Ok();
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddLoadReportDto dto)
        {
            try
            {
                await _loadReportService.UpdateAsync(dto, id);
                return Ok();
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}