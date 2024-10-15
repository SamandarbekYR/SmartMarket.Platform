using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.Interfaces.Worker.Workers;
using SmartMarket.WebApi.Controllers.Common;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/worker")]
    [ApiController]
    public class WorkerController(IWorkerService service) : ControllerBase
    {
        private readonly IWorkerService _workerService = service;
        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerDto workerDto)
        {
            await _workerService.AddAsync(workerDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok( await _workerService.GetAllAsync());


        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetWorkerByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var worker = await _workerService.GetWorkerByPhoneNumberAsync(phoneNumber);
                return Ok(worker);
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("name/{firstName}")]
        public async Task<IActionResult> GetWorkerByNameAsync(string firstName)
        {
            try
            {
                var workers = await _workerService.GetWorkerByNameAsync(firstName);
                return Ok(workers);
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(AddWorkerDto workerDto, Guid id)
        {
            try
            {
                await _workerService.UpdateAsync(workerDto, id);
                return Ok();
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _workerService.DeleteAsync(id);
                return Ok();
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
