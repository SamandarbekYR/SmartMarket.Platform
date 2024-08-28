using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.DataAccess.Repositories.Products;

public class ProductImageRepository : Repository<ProductImage>, IProductImage
{
    private readonly AppDbContext _appDbContext;
    private DbSet<ProductImage> _productImages;

    public ProductImageRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _productImages = appDb.Set<ProductImage>();
    }

    public async Task<List<ProductImage>> GetProductImagesFullInformationAsync()
    {
        return await _productImages
            .Include(pi => pi.Product)
            .ToListAsync();
    }
}
