﻿using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Debtors;
using SmartMarket.Service.Interfaces.Debtor;

namespace SmartMarket.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebtorsController(IDebtorsService debtorsService) : ControllerBase
{
    private readonly IDebtorsService _debtorService = debtorsService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var debtors = await _debtorService.GetAllAsync();
        return Ok(debtors);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddDebtorsDto dto)
    {
        await _debtorService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _debtorService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddDebtorsDto dto)
    {
        await _debtorService.UpdateAsync(dto, id);
        return Ok();
    }
}