using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.SalaryCheck;
using SmartMarket.Service.Interfaces.SalaryCheck;

namespace SmartMarket.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalaryChecksController(ISalaryCheckService salaryCheckService) : ControllerBase
{
    private readonly ISalaryCheckService _salaryCheckService = salaryCheckService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var salaryChecks = await _salaryCheckService.GetAllAsync();
        return Ok(salaryChecks);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddSalaryCheckDto dto)
    {
        await _salaryCheckService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _salaryCheckService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddSalaryCheckDto dto)
    {
        await _salaryCheckService.UpdateAsync(dto, id);
        return Ok();
    }
}