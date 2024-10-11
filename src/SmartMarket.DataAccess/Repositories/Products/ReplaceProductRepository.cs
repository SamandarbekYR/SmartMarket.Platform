using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class ReplaceProductRepository : Repository<ReplaceProduct>, IReplaceProduct
{
    private readonly AppDbContext _appDbContext;
    private DbSet<ReplaceProduct> _replaceProducts;

    public ReplaceProductRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _replaceProducts = appDb.Set<ReplaceProduct>();
    }

    public async Task<List<ReplaceProduct>> GetReplaceProductsFullInformationAsync()
    {
        return await _replaceProducts
            .Include(rp => rp.ProductSale) 
                .ThenInclude(ps => ps.Product)
            .Include(rp => rp.Worker) 
            .ToListAsync();
    }
}