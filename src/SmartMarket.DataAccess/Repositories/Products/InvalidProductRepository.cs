using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class InvalidProductRepository : Repository<InvalidProduct>, IInvalidProduct
{
    private readonly AppDbContext _appDbContext;
    private DbSet<InvalidProduct> _invalidProducts;

    public InvalidProductRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _invalidProducts = appDb.Set<InvalidProduct>();
    }

    public async Task<List<InvalidProduct>> GetInvalidProductsFullInformationAsync()
    {
        return await _invalidProducts
            .Include(ip => ip.Worker) 
            .Include(ip => ip.ProductSale) 
                .ThenInclude(ps => ps.Product)
            .Include(ip => ip.ProductSale) 
                .ThenInclude(ps => ps.SalesRequest)
            .ToListAsync();
    }
}