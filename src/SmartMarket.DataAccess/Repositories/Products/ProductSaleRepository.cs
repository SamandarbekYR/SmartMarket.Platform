using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class ProductSaleRepository : Repository<ProductSale>, IProductSale
{
    private readonly AppDbContext _appDbContext;
    private DbSet<ProductSale> _productSales;

    public ProductSaleRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _productSales = appDb.Set<ProductSale>();
    }

    public async Task<List<ProductSale>> GetProductSalesFullInformationAsync()
    {
        return await _productSales
            .Include(ps => ps.Product) 
            .Include(ps => ps.Worker) 
            .Include(ps => ps.Transaction) 
            .Include(ps => ps.PayDesk) 
            .Include(ps => ps.ReplaceProducts) 
            .Include(ps => ps.InvalidProducts) 
            .ToListAsync();
    }
}
