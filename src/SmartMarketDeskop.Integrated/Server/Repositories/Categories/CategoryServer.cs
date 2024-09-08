using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Categories;
using SmartMarketDesktop.DTOs.DTOs.Categories;
using SmartMarketDesktop.ViewModels.Entities.Categories;
using System.Net.Http.Headers;
using System.Text;
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


        public async Task<bool> DeleteAsync(Guid Id)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(AuthApi.BASE_URL + $"/api/common/categories/{Id}");

                var token = IdentitySingelton.GetInstance().Token;
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                var result = await client.DeleteAsync(client.BaseAddress);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<List<CategoryView>> GetAllAsync()
        {

            try
            {
                // HttpClient yaratish
                HttpClient client = new HttpClient();

                // Tokenni qo'shish
                var token = IdentitySingelton.GetInstance().Token;

                // API so'rovining bazaviy manzilini sozlash
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/common/categories");

                // Authorization headerni qo'shish
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                // Get so'rovini amalga oshirish
                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                // So'rov natijasini o'qish
                string response = await message.Content.ReadAsStringAsync();

                // JSON ni CategoryView listiga deserialize qilish
                List<CategoryView> posts = JsonConvert.DeserializeObject<List<CategoryView>>(response)!;

                return posts;
            }
            catch
            {
                // Xatolik yuz bergan taqdirda bo'sh ro'yxat qaytarish
                return new List<CategoryView>();
            }

        }

        public async Task<bool> UpdateAsync(CategoryDto dto, Guid Id)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var client = new HttpClient();
                var url = $"{AuthApi.BASE_URL}/api/common/categories/{Id}";
                var request = new HttpRequestMessage(HttpMethod.Put, url);
                request.Headers.Add("Authorization", $"Bearer {token}");

                // Create JSON content
                var jsonContent = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

                request.Content = jsonContent;

                var response = await client.SendAsync(request);

                var res1 = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var res = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }



        }
    }
}
