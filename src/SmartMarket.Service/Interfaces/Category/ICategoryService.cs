using SmartMarket.Service.DTOs.Category;
using Entity = SmartMarket.Domain.Entities.Categories;

namespace SmartMarket.Service.Interfaces.Category
{
    public interface ICategoryService
    {
        List<Entity.Category> GetAll();
        bool Add(CategoryDto dto);
        bool Delete(Guid Id);
        bool Update(CategoryDto dto, Guid Id);
    }
}
