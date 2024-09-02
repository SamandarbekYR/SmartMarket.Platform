using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.Interfaces.PartnersCompany.ContrAgent;

namespace SmartMarket.WebApi.Controllers.Common.PartnersCompany;

[Route("api/contr-agents")]
[ApiController]
public class ContrAgentsController(IContrAgentService contrAgentService) : ControllerBase
{
    private readonly IContrAgentService _contrAgentService = contrAgentService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var contrAgents = await _contrAgentService.GetAllAsync();
        return Ok(contrAgents);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddContrAgentDto dto)
    {
        await _contrAgentService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _contrAgentService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddContrAgentDto dto)
    {
        await _contrAgentService.UpdateAsync(dto, id);
        return Ok();
    }
}