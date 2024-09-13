using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.Interfaces.Partner;

namespace SmartMarket.WebApi.Controllers;

[Route("api/partners")]
[ApiController]
public class PartnersController(IPartnerService partnerService) : ControllerBase
{
    private readonly IPartnerService _partnerService = partnerService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var partners = await _partnerService.GetAllAsync();
        return Ok(partners);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddPartnerDto dto)
    {
        await _partnerService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _partnerService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPartnerDto dto)
    {
        await _partnerService.UpdateAsync(dto, id);
        return Ok();
    }
}