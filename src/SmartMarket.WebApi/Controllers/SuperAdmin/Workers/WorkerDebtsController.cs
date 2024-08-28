using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Workers.WorkerDebt;
using SmartMarket.Service.Interfaces.Worker.WorkerDebt;

namespace SmartMarket.WebApi.Controllers.SuperAdmin.Workers;

[Route("api/super-admin/worker-debts")]
[ApiController]
public class WorkerDebtsController(IWorkerDebtService workerDebtService) : SuperAdminController
{
    private readonly IWorkerDebtService _workerDebtService = workerDebtService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var workerDebts = await _workerDebtService.GetAllAsync();
        return Ok(workerDebts);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddWorkerDebtDto dto)
    {
        await _workerDebtService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _workerDebtService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddWorkerDebtDto dto)
    {
        await _workerDebtService.UpdateAsync(dto, id);
        return Ok();
    }
}