using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;
using SmartMarket.Service.Interfaces.Worker.SalaryWorker;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers;

[Route("api/super-admin/salary-workers")]
[ApiController]
public class SalaryWorkersController(ISalaryWorkerService salaryWorkerService) : SuperAdminController
{
    private readonly ISalaryWorkerService _salaryWorkerService = salaryWorkerService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var salaryWorkers = await _salaryWorkerService.GetAllAsync();
        return Ok(salaryWorkers);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddSalaryWorkerDto dto)
    {
        await _salaryWorkerService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _salaryWorkerService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddSalaryWorkerDto dto)
    {
        await _salaryWorkerService.UpdateAsync(dto, id);
        return Ok();
    }
}
