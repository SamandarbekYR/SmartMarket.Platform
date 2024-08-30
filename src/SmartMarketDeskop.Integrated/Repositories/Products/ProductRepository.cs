using Microsoft.EntityFrameworkCore;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces;
using SmartMarketDeskop.Integrated.Interfaces.Products;
using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Repositories.Products
{
    public class ProductRepository : Repository<ProductView>, IProduct
    {
        private readonly AppDbContext _appDbContext;
        private DbSet<ProductView> _products; 

        public ProductRepository(AppDbContext appDb) : base(appDb)
        {
            _appDbContext = appDb;
            _products = appDb.Set<ProductView>();
        }

        public async Task<List<ProductView>> GetProductsFullInformationAsync()
        {
            return await _products 
                .Include(p => p.CategoryView)
                .Include(p => p.ContrAgentView)
                .Include(p => p.WorkerView)
                .Include(p => p.ProductImageViews)
                .Include(p => p.ProductSaleViews)
                .Include(p => p.DebttorsViews)
                .Include(p => p.LoadReportViews)
                .Include(p => p.OrderViews)
                .ToListAsync();
        }
    }
}