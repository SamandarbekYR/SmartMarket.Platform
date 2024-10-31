using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.SalesRequest;
using SmartMarket.Service.Interfaces.Products.SalesRequest;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/sales-request")]
    [ApiController]
    public class SalesRequestsController(ISalesRequestService salesRequestService) : ControllerBase
    {
        private readonly ISalesRequestService _salesRequestService = salesRequestService;

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddSalesRequestDto addSalesRequestDto)
        {
            try
            {
                var result = await _salesRequestService.AddAsync(addSalesRequestDto);
                return Ok(result);
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
        public async Task<IActionResult> FilterSalesRequestAsync(FilterSalesRequestDto filterSalesRequestDto)
        {
            try
            {
                var productSales = await _salesRequestService.FilterSalesRequestAsync(filterSalesRequestDto);
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var productSale = await _salesRequestService.GetByIdAsync(id);
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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var productSales = await _salesRequestService.GetAllAsync();
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddSalesRequestDto dto)
        {
            try
            {
                await _salesRequestService.UpdateAsync(dto, id);
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
