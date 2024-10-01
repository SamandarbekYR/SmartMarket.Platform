using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Workers.SalaryCheck;
using SmartMarket.Service.Interfaces.Worker.SalaryCheck;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/salary-checks")]
    [ApiController]
    public class SalaryChecksController(ISalaryCheckService salaryCheckService) : ControllerBase
    {
        private readonly ISalaryCheckService _salaryCheckService = salaryCheckService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var salaryChecks = await _salaryCheckService.GetAllAsync();
                return Ok(salaryChecks);
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
        public async Task<IActionResult> AddAsync([FromBody] AddSalaryCheckDto dto)
        {
            try
            {
                await _salaryCheckService.AddAsync(dto);
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
                await _salaryCheckService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddSalaryCheckDto dto)
        {
            try
            {
                await _salaryCheckService.UpdateAsync(dto, id);
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