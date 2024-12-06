using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Interfaces.Order;

namespace SmartMarket.WebApi.Controllers.Common.Order
{
    [Route("api/common/orderItems")]
    [ApiController]
    public class OrderItemsController(IOrderItemService orderItemService) : ControllerBase
    {
        private readonly IOrderItemService _orderItemService = orderItemService;

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await _orderItemService.DeleteAsync(id);
                return Ok();
            }
            catch(StatusCodeException ex)
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
