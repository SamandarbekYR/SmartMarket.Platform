using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDesktop.ViewModels.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Interfaces.Orders
{
    public interface IOrder : IRepository<OrderView>
    {
        Task<List<OrderView>> GetOrdersForSpecificWorkerAsync(Guid workerId);
        Task<List<OrderView>> GetOrdersForSpecificProductAsync(Guid productId);
        public Task<List<OrderView>> GetOrdersFullInformationAsync();
    }
}
