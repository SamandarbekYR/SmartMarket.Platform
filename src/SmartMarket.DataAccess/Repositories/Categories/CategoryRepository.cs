using SmartMarket.DataAccess.Data;
using SmartMarket.DataAccess.Interfaces.Categories;
using SmartMarket.Domain.Entities.Categories;

namespace SmartMarket.DataAccess.Repositories.Categories;

public class CategoryRepository : Repository<Category>, ICategory
{
    public CategoryRepository(AppDbContext appDb) : base(appDb)
    { }
}
