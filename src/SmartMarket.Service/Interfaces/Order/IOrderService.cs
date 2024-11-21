using SmartMarket.Service.Common.Utils;
using SmartMarket.Service.DTOs.Order;

namespace SmartMarket.Service.Interfaces.Order;

public interface IOrderService
{
    Task<bool> AddAsync(AddOrderDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<List<OrderDto>> GetAllAsync();
    Task<bool> UpdateAsync(AddOrderDto dto, Guid Id);
}
