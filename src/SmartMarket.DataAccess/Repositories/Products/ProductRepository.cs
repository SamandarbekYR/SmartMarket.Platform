using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;

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
    public IQueryable<Product> GetProductsWithRequiredInformationAsync()
    {
        return _products
            .Include(p => p.Category)
            .Include(p => p.Worker)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductSale)
            .Select(p => new Product
            {
                Id = p.Id,
                PCode = p.PCode,
                Name = p.Name,
                Barcode = p.Barcode,
                Price = p.Price,
                SellPrice = p.SellPrice,
                Count = p.Count, 
                UnitOfMeasure = p.UnitOfMeasure,
                Category = new Category
                {
                    Id = p.Category.Id,
                    Name = p.Category.Name,
                },
                Worker = new Worker
                {
                    Id = p.Worker.Id,
                    FirstName = p.Worker.FirstName,
                    LastName = p.Worker.LastName,
                },
                ProductImages = p.ProductImages.Select(img => new ProductImage
                {
                    Id = img.Id,
                    ImagePath = img.ImagePath
                }).ToList(),
                NoteAmount = p.NoteAmount
            });
    }
}