using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;

namespace SmartMarketDeskop.Integrated.Repositories.Categories;

public class CategoryRepository : Repository<CategoryView>, ICategory
{
    public CategoryRepository(AppDbContext appDb) : base(appDb)
    { }
}