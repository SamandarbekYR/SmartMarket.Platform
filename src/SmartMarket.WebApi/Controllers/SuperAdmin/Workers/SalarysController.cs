using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Workers.Salary;
using SmartMarket.Service.Interfaces.Worker.Salary;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers;

[Route("api/salaries")]
[ApiController]
public class SalarysController(ISalaryService salaryService) : ControllerBase
{
    private readonly ISalaryService _salaryService = salaryService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var salaries = await _salaryService.GetAllAsync();
        return Ok(salaries);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddSalaryDto dto)
    {
        await _salaryService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _salaryService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddSalaryDto dto)
    {
        await _salaryService.UpdateAsync(dto, id);
        return Ok();
    }
}
