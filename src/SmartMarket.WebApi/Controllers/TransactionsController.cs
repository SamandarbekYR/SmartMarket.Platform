using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Transaction;
using SmartMarket.Service.Interfaces.Transaction;

namespace SmartMarket.WebApi.Controllers;

[Route("api/transactions")]
[ApiController]
public class TransactionsController(ITransactionService transactionService) : ControllerBase
{
    private readonly ITransactionService _transactionService = transactionService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var transactions = await _transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddTransactionDto dto)
    {
        await _transactionService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _transactionService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddTransactionDto dto)
    {
        await _transactionService.UpdateAsync(dto, id);
        return Ok();
    }
}