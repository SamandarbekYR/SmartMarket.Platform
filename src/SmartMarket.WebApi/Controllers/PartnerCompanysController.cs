using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.PartnerCompany;
using SmartMarket.Service.Interfaces.PartnerCompany;

namespace SmartMarket.WebApi.Controllers;

[Route("api/partner-companies")]
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