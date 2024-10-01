using SmartMarket.Service.DTOs.Category;
using Entity = SmartMarket.Domain.Entities.Categories;

namespace SmartMarket.Service.Interfaces.Category;

public interface ICategoryService
{
    Task<List<Entity.Category>> GetAllAsync();
    Task<bool> AddAsync(CategoryDto dto);
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(CategoryDto dto, Guid Id);
    Task<CategoryDto> GetByIdAsync(Guid Id);
}
