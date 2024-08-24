using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.InvalidProduct;
using SmartMarket.Service.Interfaces.InvalidProduct;

namespace SmartMarket.WebApi.Controllers;

[Route("api/invalid-products")]
[ApiController]
public class InvalidProductsController(IInvalidProductService invalidProductService) : ControllerBase
{
    private readonly IInvalidProductService _invalidProductService = invalidProductService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var invalidProducts = await _invalidProductService.GetAllAsync();
        return Ok(invalidProducts);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddInvalidProductDto dto)
    {
        await _invalidProductService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _invalidProductService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddInvalidProductDto dto)
    {
        await _invalidProductService.UpdateAsync(dto, id);
        return Ok();
    }
}
