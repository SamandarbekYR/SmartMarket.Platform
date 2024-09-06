using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Categories.Category
{
    public class CategoryService : ICategoryService
    {
        private ICategoryServer _categoryServer;
        

        public CategoryService()
        {
            _categoryServer = new SmartMarketDeskop.Integrated.Server.Repositories.Categories.CategoryServer();
        }

        public async Task<bool> AddAsync(CategoryDto dto)
        {
            if (IsInternetAvailable())
            {
                dto.IsSynced = true;
                await _categoryServer.AddAsync(dto);

                return true;    
            }
            else
            {

                // localni bazaga saqlaymiz
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid Id)
        {
            if (!IsInternetAvailable())
            {
                await _categoryServer.DeleteAsync(Id);
                return true;
            }

            else
            {
                return false;   
            }
        }

        public async Task<List<CategoryView>> GetAllAsync()
        {
            if(IsInternetAvailable())
            {

                return await _categoryServer.GetAllAsync();
            }

            else
            {
                return new List<CategoryView>();
            }
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient()!)
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(CategoryDto dto, Guid Id)
        {
            if(!IsInternetAvailable())
            {
                await _categoryServer.UpdateAsync(dto, Id);
                return true;    
            }
            else
            {
                return false;
            }
        }
    }
}
