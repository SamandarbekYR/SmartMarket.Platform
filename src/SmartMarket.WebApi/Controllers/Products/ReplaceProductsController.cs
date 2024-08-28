using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.Interfaces.Products.ReplaceProduct;

namespace SmartMarket.WebApi.Controllers.Products;

[Route("api/replace-products")]
[ApiController]
public class ReplaceProductsController(IReplaceProductService replaceProductService) : ControllerBase
{
    private readonly IReplaceProductService _replaceProductService = replaceProductService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var replaceProducts = await _replaceProductService.GetAllAsync();
        return Ok(replaceProducts);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddReplaceProductDto dto)
    {
        await _replaceProductService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _replaceProductService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddReplaceProductDto dto)
    {
        await _replaceProductService.UpdateAsync(dto, id);
        return Ok();
    }
}