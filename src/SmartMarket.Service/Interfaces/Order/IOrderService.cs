using SmartMarket.Service.DTOs.Order;

namespace SmartMarket.Service.Interfaces.Order;

public interface IOrderService
{
    Task<bool> AddAsync(AddOrderDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<OrderDto>> GetAllAsync();
    Task<List<OrderDto>> GetByPartnerNameAsync(string searchName);
    Task<bool> UpdateAsync(UpdateOrderDto dto, Guid Id);
    Task<bool> UpdateStatusAsync(Guid Id);
}
