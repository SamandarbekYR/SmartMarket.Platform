using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Products.Product;

namespace SmartMarket.WebApi.Controllers.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] AddProductDto dto)
        {
            var productId = await _productService.AddAsync(dto); 
            return Ok(productId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _productService.DeleteAsync(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddProductDto dto)
        {
            await _productService.UpdateAsync(dto, id);
            return Ok();
        }
    }
}