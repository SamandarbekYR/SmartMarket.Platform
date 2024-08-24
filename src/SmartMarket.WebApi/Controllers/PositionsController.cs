using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.Interfaces.Positions;

namespace SmartMarket.WebApi.Controllers;

[Route("api/positions")]
[ApiController]
public class PositionsController : ControllerBase
{
    private readonly IPositionService _positionService;

    public PositionsController(IPositionService positionService)
    {
        _positionService = positionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var positions = await _positionService.GetAllAsync();
        return Ok(positions);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddPositionDto dto)
    {
        await _positionService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _positionService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddPositionDto dto) 
    {
        await _positionService.UpdateAsync(dto, id);
        return Ok();
    }
}