using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.Interfaces.PayDesks;

namespace SmartMarket.WebApi.Controllers;

[Route("api/pay-desks")]
[ApiController]
public class PayDesksController(IPayDeskService payDeskService) : ControllerBase
{
    private readonly IPayDeskService _payDeskService = payDeskService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var payDesks = await _payDeskService.GetAllAsync();
        return Ok(payDesks);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddPayDesksDto dto)
    {
        await _payDeskService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _payDeskService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPayDesksDto dto)
    {
        await _payDeskService.UpdateAsync(dto, id);
        return Ok();
    }
}