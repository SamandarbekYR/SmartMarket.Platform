using SmartMarket.Service.DTOs.Order;

namespace SmartMarketDeskop.Integrated.Services.Orders;

public interface IOrderService
{
    Task<bool> CreateAsync(AddOrderDto order);
    Task<List<OrderDto>> GetAllAsync();
    Task<List<OrderDto>> GetByPartnerNameAsync(string searchName);
    Task<bool> UpdateStatusAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateOrderDto order);
}
