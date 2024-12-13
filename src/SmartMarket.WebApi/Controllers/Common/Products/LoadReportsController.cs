﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("filter")]
        public async Task<IActionResult> FilterAsync(FilterLoadReportDto dto)
        {
            try
            {
                var loadReports = await _loadReportService.FilterLoadReportAsync(dto);
                return Ok(loadReports);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            try
            {
                var loadReport = await _loadReportService.GetByIdAsync(id);
                return Ok(loadReport);
            }
            catch(StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("contr-agent/{contrAgentId}")]
        public async Task<IActionResult> GetLoadReportsByContrAgentIdAsync([FromRoute] Guid contrAgentId)
        {
            try
            {
                var loadReports = await _loadReportService.GetLoadReportsByContrAgentIdAsync(contrAgentId);
                return Ok(loadReports);
            }
            catch(StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetLoadReportStatisticsAsync()
        {
            try
            {
                var loadReportStatistics = await _loadReportService.GetStatisticsAsync();
                return Ok(loadReportStatistics);
            }
            catch (StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            } 
            catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("collected")]
        public async Task<IActionResult> GetAllCollectedAsync()
        {
            try
            {
                var loadReports = await _loadReportService.GetAllCollectedAsync();
                return Ok(loadReports);
            }
            catch(StatusCodeException ex)
            {
                return StatusCode((int)ex.StatusCode, ex.Message);
            }
            catch(Exception ex)
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