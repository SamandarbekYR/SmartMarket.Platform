using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.ProductSale;
using SmartMarket.Service.Interfaces.ProductSale;

namespace SmartMarket.WebApi.Controllers;

[Route("api/product-sales")]
[ApiController]
public class ProductSalesController(IProductSaleService productSaleService) : ControllerBase
{
    private readonly IProductSaleService _productSaleService = productSaleService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var productSales = await _productSaleService.GetAllAsync();
        return Ok(productSales);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddProductSaleDto dto)
    {
        await _productSaleService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _productSaleService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddProductSaleDto dto)
    {
        await _productSaleService.UpdateAsync(dto, id);
        return Ok();
    }
}