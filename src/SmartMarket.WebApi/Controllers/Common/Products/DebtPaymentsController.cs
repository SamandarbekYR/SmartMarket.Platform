using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.DTOs.Products.DebtPayment;
using SmartMarket.Service.Interfaces.Products.DebtPayment;

namespace SmartMarket.WebApi.Controllers.Common.Products
{
    [Route("api/debt-payments")]
    [ApiController]
    public class DebtPaymentsController(IDebtPaymentService debtPaymentService) : ControllerBase
    {
        private readonly IDebtPaymentService _debtPaymentService = debtPaymentService;

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var debtPayments = await _debtPaymentService.GetAllAsync();
                return Ok(debtPayments);
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
        public async Task<IActionResult> AddAsync([FromBody] AddDebtPaymentDto dto)
        {
            try
            {
                await _debtPaymentService.AddAsync(dto);
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
                await _debtPaymentService.DeleteAsync(id);
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
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddDebtPaymentDto dto)
        {
            try
            {
                await _debtPaymentService.UpdateAsync(dto, id);
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