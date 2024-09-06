using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Categories.Category
{
    public interface ICategoryService
    {
        Task<List<CategoryView>> GetAllAsync();
        Task<bool> AddAsync(CategoryDto dto);
        Task<bool> DeleteAsync(Guid Id);
        Task<bool> UpdateAsync(CategoryDto dto, Guid Id);
    }
}
