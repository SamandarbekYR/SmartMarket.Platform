using SmartMarketDeskop.Integrated.Api.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Auth
{
    public class JwtSendToken
    {
        public async static void Send(string Token)
        {
            // Kerakli ma'lumotlarni o'zgartiring
            string apiEndpoint = $"{AuthApi.BASE_URL}" + "/api/client/storages";

            string token = Token;

            var httpClient = new HttpClient();

            // Post so'rovi uchun sarlavhaga "Authorization: Bearer {token}" qo'shamiz
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Post so'rov tayyorlash
            var postContent = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json");

            // Post so'rovni yuborish
            var response = await httpClient.PostAsync(apiEndpoint, postContent);

            // Javobni olish va chiqarish
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API javobi: " + responseContent);
        }
    }
}
