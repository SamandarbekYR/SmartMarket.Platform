using Microsoft.AspNetCore.Mvc;

using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Scales;
using SmartMarket.Service.Interfaces.Scales;

namespace SmartMarket.WebApi.Controllers.Common.Scales
{
    [Route("api/scales")]
    [ApiController]
    public class ScalesController(IScaleService scaleService) : ControllerBase
    {
        private readonly IScaleService _scaleService = scaleService;


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var scales = await _scaleService.GetAllAsync();
                return Ok(scales);
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
        public async Task<IActionResult> AddAsync([FromBody] AddScaleDto dto)
        {
            try
            {
                await _scaleService.AddAsync(dto);
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
                await _scaleService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddScaleDto dto)
        {
            try
            {
                await _scaleService.UpdateAsync(dto, id);
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
