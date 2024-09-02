using Microsoft.Extensions.Logging;
using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
namespace SmartMarketDeskop.Integrated.Server.Repositories.Categories
{
    public class CategoryServer : ICategoryServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<bool> AddAsync(CategoryDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/common/categories"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    // Check if the API expects JSON content instead of multipart form data
                    var content = new MultipartFormDataContent
                    {
                        { new StringContent(dto.Name), "name" },
                        { new StringContent(dto.Description), "description" }
                    };

                    request.Content = content;

                    var response = await client.SendAsync(request);

                    // Read response content
                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        // Log the response status and content for debugging
                        logger.Error($"Failed to add category. Status Code: {response.StatusCode}, Response: {resultContent}");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("CategoryServer: AddAsync method exception: ", ex);
                return false;
            }
        }


        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryView>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CategoryDto dto, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}
