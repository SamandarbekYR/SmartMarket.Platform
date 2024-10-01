using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.Interfaces.PayDesks;
using SmartMarket.WebApi.Controllers.Common;

namespace SmartMarket.WebApi.Controllers.Common.PayDesk
{
    [Route("api/pay-desks")]
    [ApiController]
    public class PayDesksController(IPayDeskService payDeskService) : ControllerBase
    {
        private readonly IPayDeskService _payDeskService = payDeskService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var payDesks = await _payDeskService.GetAllAsync();
                return Ok(payDesks);
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
        public async Task<IActionResult> AddAsync([FromBody] AddPayDesksDto dto)
        {
            try
            {
                await _payDeskService.AddAsync(dto);
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
                await _payDeskService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPayDesksDto dto)
        {
            try
            {
                await _payDeskService.UpdateAsync(dto, id);
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