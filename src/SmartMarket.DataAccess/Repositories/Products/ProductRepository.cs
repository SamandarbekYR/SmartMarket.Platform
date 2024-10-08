using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class ProductRepository : Repository<Product>, IProduct
{
    private readonly AppDbContext _appDbContext;
    private DbSet<Product> _products;

    public ProductRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _products = appDb.Set<Product>();
    }

    public async Task<List<Product>> GetProductsFullInformationAsync()
    {
        return await _products
            .Include(p => p.Category) 
            .Include(p => p.ContrAgent) 
            .Include(p => p.Worker) 
            .Include(p => p.ProductImages) 
            .Include(p => p.ProductSale) 
            .Include(p => p.Debtors) 
            .Include(p => p.LoadReport) 
            .Include(p => p.Order) 
            .ToListAsync();
    }
    public IQueryable<Product> GetAllProductsFullInformation()
    {
        return _products
            .Include(p => p.Category)
            .Include(p => p.ContrAgent)
            .Include(p => p.Worker)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductSale)
            .Include(p => p.Debtors)
            .Include(p => p.LoadReport)
            .Include(p => p.Order);
    }
}