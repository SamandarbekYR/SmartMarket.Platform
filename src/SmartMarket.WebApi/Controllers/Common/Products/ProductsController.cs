using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Products.Product;
using SmartMarket.WebApi.Controllers.Common;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService) : BaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
            => Ok(await _productService.GetAllAsync(@params));

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

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetProductByBarcodeAsync(string barcode)
        {
            try
            {
                var product = await _productService.GetProductByBarcodeAsync(barcode);
                return Ok(product);
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

        [HttpGet("pcode/{pCode}")]
        public async Task<IActionResult> GetProductByPCodeAsync(string pCode)
        {
            try
            {
                var product = await _productService.GetProductByPCodeAsync(pCode);
                return Ok(product);
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

        [HttpGet("worker/{workerId}")]
        public async Task<IActionResult> GetProductByWorkerAsync(Guid workerId)
        {
            try
            {
                var product = await _productService.GetProductByWorkerIdAsync(workerId);
                return Ok(product);
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
        public async Task<IActionResult> SearchProductsAsync([FromQuery] string searchTerm, [FromQuery] PaginationParams @params)
        {
            try
            {
                var products = await _productService.SearchProductsAsync(searchTerm, @params);
                return Ok(products);
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
    }
}