using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.Interfaces.Products.Product;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            try
            {
                var products = await _productService.GetAllAsync(@params);
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

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromForm] AddProductDto dto)
        {
            try
            {
                var productId = await _productService.AddAsync(dto);
                return Ok(productId);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _productService.DeleteAsync(id);
                return Ok();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddProductDto dto)
        {
            try
            {
                await _productService.UpdateAsync(dto, id);
                return Ok();
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

        [HttpPut("update-count")]
        public async Task<IActionResult> UpdateProductCountAsync([FromBody] List<UpdateProductDto> items)
        {
            if (items == null || !items.Any())
                return BadRequest("The items list cannot be null or empty.");

            try
            {
                var result = await _productService.UpdateProductCountAsync(items);

                if (!result)
                    return BadRequest("Failed to update product count.");

                return Ok(result);
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the product count.");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
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

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetProductByNameAsync(string name)
        {
            try
            {
                var product = await _productService.GetProductsByNameAsync(name);
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

        [HttpPost("sell")]
        public async Task<IActionResult> SellProductAsync([FromQuery] string barcode)
        {
            try
            {
                var success = await _productService.SellProductAsync(barcode);
                return Ok(success);
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

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryIdAsync(Guid categoryId, [FromQuery] PaginationParams @params)
        {
            try
            {
                var products = await _productService.GetProductsByCategoryIdAsync(categoryId, @params);
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] PaginationParams @params)
        {
            try
            {
                var products = await _productService.GetProductsFullInformationAsync(@params);
                return Ok(products);
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

        [HttpGet("finished")]
        public async Task<IActionResult> GetFinishedProductsAsync([FromQuery] PaginationParams @params)
        {
            try
            {
                var finishedProducts = await _productService.GetFinishedProductsAsync(@params);
                return Ok(finishedProducts);
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
