using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.Interfaces.Customer;

namespace SmartMarket.WebApi.Controllers.Common.Customer;

[Route("api/customers")]
[ApiController]
public class CustomersController(ICustomerService customerService) : ControllerBase
{
    private readonly ICustomerService _customerService = customerService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var customers = await _customerService.GetAllAsync();
        return Ok(customers);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddCustomerDto dto)
    {
        await _customerService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _customerService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddCustomerDto dto)
    {
        await _customerService.UpdateAsync(dto, id);
        return Ok();
    }
}