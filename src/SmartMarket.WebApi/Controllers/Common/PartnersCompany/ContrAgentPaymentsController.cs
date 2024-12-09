using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgentPayment;

namespace SmartMarket.WebApi.Controllers.Common.PartnersCompany
{
    [Route("api/common/contr-agent-payments")]
    [ApiController]
    public class ContrAgentPaymentsController(IContrAgentPaymentService contrAgentPaymentService) : ControllerBase
    {
        private readonly IContrAgentPaymentService _contrAgentPaymentService = contrAgentPaymentService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var contrAgentPayments = await _contrAgentPaymentService.GetAllAsync();
                return Ok(contrAgentPayments);
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
        public async Task<IActionResult> AddAsync([FromBody] AddContrAgentPaymentDto dto)
        {
            try
            {
                await _contrAgentPaymentService.AddAsync(dto);
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

        [HttpPost("filter")]
        public async Task<IActionResult> FilterContrAgentPaymentsAsync([FromBody] FilterContrAgentDto dto)
        {
            try
            {
                var contrAgentPayments = await _contrAgentPaymentService.FilterContrAgentPaymentAsync(dto);
                return Ok(contrAgentPayments);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _contrAgentPaymentService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddContrAgentPaymentDto dto)
        {
            try
            {
                await _contrAgentPaymentService.UpdateAsync(dto, id);
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