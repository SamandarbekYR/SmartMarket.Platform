using Microsoft.Extensions.Logging;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.Service.Common.Exceptions;
using SmartMarket.Service.Interfaces.Order;
using System.Net;

namespace SmartMarket.Service.Services.Order;

public class OrderItemService(
    IUnitOfWork unitOfWork,
    ILogger<OrderItemService> logger) : IOrderItemService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILogger<OrderItemService> _logger = logger;
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var orderItem = await _unitOfWork.OrderItem.GetById(id);

            if (orderItem == null) 
                throw new StatusCodeException(HttpStatusCode.NotFound, "Order item not found.");

            return await _unitOfWork.OrderItem.Remove(orderItem);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while deteting an order item.");
            throw;
        }
    }
}
