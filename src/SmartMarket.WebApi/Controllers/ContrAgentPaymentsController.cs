using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.ContrAgentPayment;
using SmartMarket.Service.Interfaces.ContrAgentPayment;

namespace SmartMarket.WebApi.Controllers;

[Route("api/contr-agent-payments")]
[ApiController]
public class ContrAgentPaymentsController(IContrAgentPaymentService contrAgentPaymentService) : ControllerBase
{
    private readonly IContrAgentPaymentService _contrAgentPaymentService = contrAgentPaymentService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var contrAgentPayments = await _contrAgentPaymentService.GetAllAsync();
        return Ok(contrAgentPayments);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddContrAgentPaymentDto dto)
    {
        await _contrAgentPaymentService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _contrAgentPaymentService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddContrAgentPaymentDto dto)
    {
        await _contrAgentPaymentService.UpdateAsync(dto, id);
        return Ok();
    }
}
