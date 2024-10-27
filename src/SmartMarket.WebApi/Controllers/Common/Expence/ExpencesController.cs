using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.Interfaces.Expence;

namespace SmartMarket.WebApi.Controllers.Common.Expence;

[Route("api/expences")]
[ApiController]
public class ExpencesController(IExpenceService expenceService) : ControllerBase
{
    private readonly IExpenceService _expenceService = expenceService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams paginationParams)
    {
        try
        {
            var expenses = await _expenceService.GetAllAsync(paginationParams);
            return Ok(expenses);
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddExpenceDto dto)
    {
        try
        {
            await _expenceService.AddAsync(dto);
            return Ok();
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost("filter")]
    public async Task<IActionResult> FilterExpenceAsync([FromBody] FilterExpenseDto filterExpenseDto)
    {
        try
        {
            var expences = await _expenceService.FilterExpenceAsync(filterExpenseDto);
            return Ok(expences);
        }
        catch (StatusCodeException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        try
        {
            var expense = await _expenceService.GetByIdAsync(id);
            return Ok(expense);
        }
        catch(StatusCodeException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
    

    [HttpGet("reason/{reason}")]
    public async Task<IActionResult> GetExpensesByReasonAsync(string reason, [FromQuery] PaginationParams @params)
    {
        try
        {
            var expenses = await _expenceService.GetExpensesByReasonAsync(reason, @params);
            return Ok(expenses);
        }
        catch (StatusCodeException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("summary")]
    public async Task<IActionResult> GetExpenseSummaryAsync()
    {
        try
        {
            var expenseSummary = await _expenceService.GetExpenseSummaryAsync();
            return Ok(expenseSummary);
        }
        catch(StatusCodeException ex)
        {
            return NotFound(ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        try
        {
            await _expenceService.DeleteAsync(id);
            return Ok();
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddExpenceDto dto)
    {
        try
        {
            await _expenceService.UpdateAsync(dto, id);
            return Ok();
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}