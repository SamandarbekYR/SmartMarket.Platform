using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Workers.WorkerDebt;
using SmartMarket.Service.Interfaces.Worker.WorkerDebt;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/worker-debts")]
    [ApiController]
    public class WorkerDebtsController(IWorkerDebtService workerDebtService) : ControllerBase
    {
        private readonly IWorkerDebtService _workerDebtService = workerDebtService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var workerDebts = await _workerDebtService.GetAllAsync();
                return Ok(workerDebts);
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
        public async Task<IActionResult> AddAsync([FromBody] AddWorkerDebtDto dto)
        {
            try
            {
                await _workerDebtService.AddAsync(dto);
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
                await _workerDebtService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddWorkerDebtDto dto)
        {
            try
            {
                await _workerDebtService.UpdateAsync(dto, id);
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