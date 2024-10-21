using Microsoft.AspNetCore.Mvc;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.Interfaces.Products.ProductSale;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/product-sales")]
    [ApiController]
    public class ProductSalesController(IProductSaleService productSaleService) : ControllerBase
    {
        private readonly IProductSaleService _productSaleService = productSaleService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var productSales = await _productSaleService.GetAllAsync();
                return Ok(productSales);
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
        public async Task<IActionResult> AddAsync([FromBody] AddProductSaleDto dto)
        {
            try
            {
                await _productSaleService.AddAsync(dto);
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

        [HttpGet("product/{productName}")]
        public async Task<IActionResult> GetProductSalesByProductNameAsync(string productName)
        {
            try
            {
                var productSales = await _productSaleService.GetProductSalesByProductNameAsync(productName);
                return Ok(productSales);
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

        [HttpGet("daterange/{fromDate}/{toDate}")]
        public async Task<IActionResult> GetProductSalesByDateRangeAsync(DateTime fromDate, DateTime toDate)
        {
            try
            {
                var productSales = await _productSaleService.GetProductSalesByDateRangeAsync(fromDate, toDate);
                return Ok(productSales);
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


        [HttpGet("transaction/{transactionId}")]
        public async Task<IActionResult> GetProductSalesByTransactionAsync(Guid transactionId)
        {
            try
            {
                var productSales = await _productSaleService.GetProductSalesByTransactionAsync(transactionId);
                return Ok(productSales);
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
                await _productSaleService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddProductSaleDto dto)
        {
            try
            {
                await _productSaleService.UpdateAsync(dto, id);
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

        [HttpPost("filter")]
        public async Task<IActionResult> FilterProductSaleAsync([FromBody] FilterProductSaleDto dto)
        {
            try
            {
                var productSales = await _productSaleService.FilterProductSaleAsync(dto);
                return Ok(productSales);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var productSale = await _productSaleService.GetByIdAsync(id);
                return Ok(productSale);
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