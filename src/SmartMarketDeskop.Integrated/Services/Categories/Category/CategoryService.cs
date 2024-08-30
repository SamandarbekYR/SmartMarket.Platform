using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SmartMarketDeskop.Integrated.DBContext;
using SmartMarketDeskop.Integrated.Interfaces.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Categories.Category
{
    public class CategoryService 
    {
        string Base_Url = "http://localhost:5293/";
        private readonly AppDbContext localDbContext;
        private DbSet<CategoryView> _dbset;
        public CategoryService(AppDbContext appDbContext)
        {
            localDbContext = appDbContext;
            _dbset =localDbContext.Set<CategoryView>();
        }
        public async Task<bool> CreateCategory(CategoryView categoryView)
        {
            if (IsInternetAvailable())
            {
                try
                {
                    HttpClient client = new HttpClient();
                    HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Base_Url + "/api/categories");

                    categoryView.IsSynced= true;
                    var json = JsonConvert.SerializeObject(categoryView);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    httpRequestMessage.Content = content;

                    var response = await client.SendAsync(httpRequestMessage);
                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }

                    return false;
                }
                catch { return false; }
            }

            else
            {
                categoryView.IsSynced = false;

                await _dbset.AddAsync(categoryView);
                await localDbContext.SaveChangesAsync();
                return true;
            }

            return false ;
        }

        public Task<bool> DeleteCategory(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryView>> GetAllCategory()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategory(CategoryView categoryView)
        {
            throw new NotImplementedException();
        }

        public bool IsInternetAvailable()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
