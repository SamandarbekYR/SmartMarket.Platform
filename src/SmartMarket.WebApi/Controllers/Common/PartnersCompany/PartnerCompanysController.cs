using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarket.Service.Interfaces.PartnersCompany.PartnerCompany;

namespace SmartMarket.WebApi.Controllers.Common.PartnersCompany;

[Route("api/common/partner-companies")]
[ApiController]
public class PartnerCompanysController(IPartnerCompanyService partnerCompanyService) : ControllerBase
{
    private readonly IPartnerCompanyService _partnerCompanyService = partnerCompanyService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var partnerCompanies = await _partnerCompanyService.GetAllAsync();
        return Ok(partnerCompanies);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddPartnerCompanyDto dto)
    {
        await _partnerCompanyService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _partnerCompanyService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPartnerCompanyDto dto)
    {
        await _partnerCompanyService.UpdateAsync(dto, id);
        return Ok();
    }
}