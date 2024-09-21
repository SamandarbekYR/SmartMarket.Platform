using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Products.ProductImage;
using SmartMarket.Service.Interfaces.Products.ProductImage;

namespace SmartMarket.WebApi.Controllers.Common.Products;

[Route("api/product-images")]
[ApiController]
public class ProductImagesController : ControllerBase
{
    private readonly IProductImageService _productImageService;

    public ProductImagesController(IProductImageService productImageService)
    {
        _productImageService = productImageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var productImages = await _productImageService.GetAllAsync();
        return Ok(productImages);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromForm] AddProductImageDto dto)
    {
        if (dto.ImagePath == null || dto.ImagePath.Length == 0)
        {
            return BadRequest("Image is required.");
        }

        await _productImageService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _productImageService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromForm] AddProductImageDto dto)
    {
        if (dto.ImagePath == null || dto.ImagePath.Length == 0)
        {
            return BadRequest("Image is required.");
        }

        await _productImageService.UpdateAsync(dto, id);
        return Ok();
    }
}