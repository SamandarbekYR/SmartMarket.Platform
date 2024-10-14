using Microsoft.EntityFrameworkCore;
using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Products;
using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.PartnersCompany;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.DataAccess.Repositories.Products;

public class LoadReportRepository : Repository<LoadReport>, ILoadReport
{
    private readonly AppDbContext _appDbContext;
    private DbSet<LoadReport> _loadReports;

    public LoadReportRepository(AppDbContext appDb) : base(appDb)
    {
        _appDbContext = appDb;
        _loadReports = appDb.Set<LoadReport>();
    }

    public IQueryable<LoadReport> GetLoadReportsFullInformationAsync()
    {
        return _loadReports
            .Include(lr => lr.Worker)
            .Include(lr => lr.Product)
                .ThenInclude(p => p.Category)
            .Include(lr => lr.ContrAgent)
            .Select(lr => new LoadReport
            {
                Id = lr.Id,
                TotalPrice = lr.Product.SellPrice * lr.Product.Count, 
                Product = new Product
                {
                    Id = lr.Product.Id,
                    PCode = lr.Product.PCode,
                    Barcode = lr.Product.Barcode,
                    Name = lr.Product.Name,
                    Category = new Category
                    {
                        Id = lr.Product.Category.Id,
                        Name = lr.Product.Category.Name,
                    },
                    SellPrice = lr.Product.SellPrice,
                    Count = lr.Product.Count,
                    UnitOfMeasure = lr.Product.UnitOfMeasure,
                    ContrAgent = new ContrAgent
                    {
                        FirstName = lr.ContrAgent.FirstName,
                        LastName = lr.ContrAgent.LastName,
                        PhoneNumber = lr.ContrAgent.PhoneNumber
                    }
                },
                Worker = new Worker
                {
                    Id = lr.Worker.Id,
                    FirstName = lr.Worker.FirstName,
                    LastName = lr.Worker.LastName,
                    PhoneNumber = lr.Worker.PhoneNumber,
                    ImgPath = lr.Worker.ImgPath
                },
                ContrAgent = new ContrAgent
                {
                    FirstName = lr.ContrAgent.FirstName,
                    LastName = lr.ContrAgent.LastName,
                    PhoneNumber = lr.ContrAgent.PhoneNumber
                }
            });
    }

}