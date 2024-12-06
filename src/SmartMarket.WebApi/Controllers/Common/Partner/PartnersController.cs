using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.Interfaces.Partner;

namespace SmartMarket.WebApi.Controllers.Common.Partner
{
    [Route("api/partners")]
    [ApiController]
    public class PartnersController(IPartnerService partnerService) : ControllerBase
    {
        private readonly IPartnerService _partnerService = partnerService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var partners = await _partnerService.GetAllAsync();
                return Ok(partners);
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
        public async Task<IActionResult> AddAsync([FromBody] AddPartnerDto dto)
        {
            try
            {
                await _partnerService.AddAsync(dto);
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
                await _partnerService.DeleteAsync(id);
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

        [HttpGet("phone/{phoneNumber}")]
        public async Task<IActionResult> GetPartnerByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                var partner = await _partnerService.GetPartnerByPhoneNumberAsync(phoneNumber);
                return Ok(partner);
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
        public async Task<IActionResult> GetPartnerByFirstNameAsync(string firstName)
        {
            try
            {
                var partner = await _partnerService.GetPartnerByFirstNameAsync(firstName);
                return Ok(partner);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPartnerDto dto)
        {
            try
            {
                await _partnerService.UpdateAsync(dto, id);
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

        [HttpPut("debt-sum/{id}")]
        public async Task<IActionResult> UpdateDebtSumAsync(Guid id, [FromBody] double debtSum)
        {
            try
            {
                await _partnerService.UpdatePartnerDebtSumAsync(debtSum, id);
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