using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Orders;
using SmartMarketDesktop.ViewModels.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Orders
{
    public class OrderRepository : Repository<OrderView>, IOrder
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<OrderView> _orders;

        public OrderRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _orders = appDb.Set<OrderView>();
        }

        public async Task<List<OrderView>> GetOrdersForSpecificWorkerAsync(Guid workerId)
        {
            return await _orders
                .Where(o => o.WorkerViewId == workerId)
                .Include(o => o.WorkerView)
                .Include(o => o.ProductView)
                .ToListAsync();
        }

        public async Task<List<OrderView>> GetOrdersForSpecificProductAsync(Guid productId)
        {
            return await _orders
                .Where(o => o.ProductViewId == productId)
                .Include(o => o.WorkerView)
                .Include(o => o.ProductView)
                .ToListAsync();
        }

        public async Task<List<OrderView>> GetOrdersFullInformationAsync()
        {
            return await _orders
                .Include(o => o.WorkerView)
                .Include(o => o.ProductView)
                .ToListAsync();
        }
    }
}
