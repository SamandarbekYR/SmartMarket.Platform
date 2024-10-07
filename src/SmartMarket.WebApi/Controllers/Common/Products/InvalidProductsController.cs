using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.Interfaces.Products.InvalidProduct;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/invalid-products")]
    [ApiController]
    public class InvalidProductsController(IInvalidProductService invalidProductService) : ControllerBase
    {
        private readonly IInvalidProductService _invalidProductService = invalidProductService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var invalidProducts = await _invalidProductService.GetAllAsync();
                return Ok(invalidProducts);
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
        public async Task<IActionResult> AddAsync([FromBody] AddInvalidProductDto dto)
        {
            try
            {
                await _invalidProductService.AddAsync(dto);
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

        /*[HttpGet("product/{productName}")]
        public async Task<IActionResult> GetInvalidProductsByProductNameAsync(string productName)
        {
            try
            {
                var invalidProducts = await _invalidProductService.GetInvalidProductsByProductNameAsync(productName);
                return Ok(invalidProducts);
            }
            catch (StatusCodeException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }*/

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _invalidProductService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddInvalidProductDto dto)
        {
            try
            {
                await _invalidProductService.UpdateAsync(dto, id);
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
}