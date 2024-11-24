using SmartMarket.Service.DTOs.Order;

namespace SmartMarketDeskop.Integrated.Server.Interfaces.Orders
{
    public interface IOrderServer
    {
        Task<bool> CreateAsync(AddOrderDto expence);
        Task<List<OrderDto>> GetAllAsyc();
        Task<bool> UpdateStatusAsync(Guid Id);
    }
}
