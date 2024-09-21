using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Products.DebtPayment;
using SmartMarket.Service.Interfaces.Products.DebtPayment;

namespace SmartMarket.WebApi.Controllers.Common.Products;

[Route("api/debt-payments")]
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