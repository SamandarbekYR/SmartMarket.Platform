using SmartMarket.Service.DTOs.Order;

namespace SmartMarketDeskop.Integrated.Services.Orders;

public interface IOrderService
{
    Task<bool> CreateAsync(AddOrderDto order);
    Task<bool> UpdateStatusAsync(Guid id);
    Task<List<OrderDto>> GetAllAsync();
}
