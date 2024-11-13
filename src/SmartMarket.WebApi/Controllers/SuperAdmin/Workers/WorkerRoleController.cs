using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Workers.WorkerRole;
using SmartMarket.Service.Interfaces.Worker.WorkerRole;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers
{
    [Route("api/super-admin/worker-role")]
    [ApiController]
    public class WorkerRoleController(IWorkerRoleService workerRole) : ControllerBase
    {
        public IWorkerRoleService _workerRole = workerRole;

        [HttpPost]
        public async Task<IActionResult> Add(AddWorkerRoleDto workerRoleDto)
        {
            try
            {
                var result = await _workerRole.AddAsync(workerRoleDto);
                return Ok(result);
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _workerRole.GetAllAsync();
                return Ok(result);
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

        [HttpPut]
        public async Task<IActionResult> Update(AddWorkerRoleDto dto, Guid Id)
        {
            try
            {
                var result = await _workerRole.UpdateAsync(dto, Id);
                return Ok(result);
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

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                var result = await _workerRole.DeleteAsync(Id);
                return Ok(result);
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
