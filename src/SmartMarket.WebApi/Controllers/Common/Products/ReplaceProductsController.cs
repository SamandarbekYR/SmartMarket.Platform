using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.Interfaces.Products.ReplaceProduct;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/replace-products")]
    [ApiController]
    public class ReplaceProductsController(IReplaceProductService replaceProductService) : ControllerBase
    {
        private readonly IReplaceProductService _replaceProductService = replaceProductService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var replaceProducts = await _replaceProductService.GetAllAsync();
                return Ok(replaceProducts);
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
        public async Task<IActionResult> AddAsync([FromBody] AddReplaceProductDto dto)
        {
            try
            {
                await _replaceProductService.AddAsync(dto);
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _replaceProductService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddReplaceProductDto dto)
        {
            try
            {
                await _replaceProductService.UpdateAsync(dto, id);
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