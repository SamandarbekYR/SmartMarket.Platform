using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgent;

namespace SmartMarket.WebApi.Controllers.Common.PartnersCompany
{
    [Route("api/common/contr-agents")]
    [ApiController]
    public class ContrAgentsController(IContrAgentService contrAgentService) : ControllerBase
    {
        private readonly IContrAgentService _contrAgentService = contrAgentService;

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddContrAgentDto dto)
        {
            try
            {
                await _contrAgentService.AddAsync(dto);
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            try
            {
                var contrAgents = await _contrAgentService.GetAllAsync(@params);
                return Ok(contrAgents);
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

        [HttpGet("company/{companyName}")]
        public async Task<IActionResult> GetContrAgentByCompanyNameAsync(string companyName)
        {
            try
            {
                var contrAgent = await _contrAgentService.GetContrAgentByCompanyNameAsync(companyName);
                return Ok(contrAgent);
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
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _contrAgentService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddContrAgentDto dto)
        {
            try
            {
                await _contrAgentService.UpdateAsync(dto, id);
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
        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetContrAgentByNameAsync(string name)
        {
            try
            {
                var contrAgent = await _contrAgentService.GetContrAgentByNameAsync(name);
                return Ok(contrAgent);
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

        [HttpGet("number/{number}")]
        public async Task<IActionResult> GetContrAgentByNumberAsync(string number)
        {
            try
            {
                var contrAgent = await _contrAgentService.GetContrAgentByNumberAsync(number);
                return Ok(contrAgent);
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