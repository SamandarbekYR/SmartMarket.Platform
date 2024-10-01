using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarket.Service.Interfaces.PartnersCompany.PartnerCompany;

namespace SmartMarket.WebApi.Controllers.Common.PartnersCompany
{
    [Route("api/common/partner-companies")]
    [ApiController]
    public class PartnerCompanysController(IPartnerCompanyService partnerCompanyService) : ControllerBase
    {
        private readonly IPartnerCompanyService _partnerCompanyService = partnerCompanyService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var partnerCompanies = await _partnerCompanyService.GetAllAsync();
                return Ok(partnerCompanies);
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
        public async Task<IActionResult> AddAsync([FromBody] AddPartnerCompanyDto dto)
        {
            try
            {
                await _partnerCompanyService.AddAsync(dto);
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
                await _partnerCompanyService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPartnerCompanyDto dto)
        {
            try
            {
                await _partnerCompanyService.UpdateAsync(dto, id);
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