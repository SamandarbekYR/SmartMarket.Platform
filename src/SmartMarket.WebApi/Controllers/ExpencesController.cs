using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.Interfaces.Expence;

namespace SmartMarket.WebApi.Controllers;

[Route("api/expences")]
[ApiController]
public class ExpencesController(IExpenceService expenceService) : ControllerBase
{
    private readonly IExpenceService _expenceService = expenceService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var expenses = await _expenceService.GetAllAsync();
        return Ok(expenses);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddExpenceDto dto)
    {
        await _expenceService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _expenceService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddExpenceDto dto)
    {
        await _expenceService.UpdateAsync(dto, id);
        return Ok();
    }
}