using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Orders;
using SmartMarket.Domain.Entities.Orders;

namespace SmartMarket.DataAccess.Repositories.Orders;

public class OrderRepository : Repository<Order>, IOrder
{
    private readonly AppDbContext _appDbContext;
    private DbSet<Order> _orders;

    public OrderRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _orders = appDb.Set<Order>();
    }

    public async Task<List<Order>> GetOrdersFullInformationAsync()
    {
        return await _orders
                .Include(sr => sr.Worker)
                .Include(sr => sr.Partner)
                .Include(sr => sr.ProductOrderItems)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.Category)
                .ToListAsync();
    }

    public IQueryable<Order> GetOrdersFull()
    {
        return _orders
                .Include(sr => sr.Worker)
                .Include(sr => sr.Partner)
                .Include(sr => sr.ProductOrderItems)
                    .ThenInclude(ps => ps.Product)
                        .ThenInclude(p => p.Category)
                .AsQueryable();
    }
}
