﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.DebtPayment;
using SmartMarket.Service.Interfaces.DebtPayment;

namespace SmartMarket.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DebtPaymentsController(IDebtPaymentService debtPaymentService) : ControllerBase
{
    private readonly IDebtPaymentService _debtPaymentService = debtPaymentService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var debtPayments = await _debtPaymentService.GetAllAsync();
        return Ok(debtPayments);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddDebtPaymentDto dto)
    {
        await _debtPaymentService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _debtPaymentService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddDebtPaymentDto dto)
    {
        await _debtPaymentService.UpdateAsync(dto, id);
        return Ok();
    }
}