using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class ProductImageRepository : Repository<ProductImageView>, IProductImage
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ProductImageView> _productImages;

        public ProductImageRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _productImages = appDb.Set<ProductImageView>();
        }

        public async Task<List<ProductImageView>> GetProductImagesFullInformationAsync()
        {
            return await _productImages
                .Include(pi => pi.ProductView)
                .ToListAsync();
        }
    }
}