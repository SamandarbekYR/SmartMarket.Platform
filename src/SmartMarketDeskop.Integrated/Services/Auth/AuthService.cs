using Newtonsoft.Json;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Interfaces.Auth;
using SmartMarketDesktop.DTOs.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Services.Auth
{
    public class AuthService : IAuthService
    {
        public async Task<(bool Result, string Token)> LoginAsync(UserLoginDto dto)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{AuthApi.LoginApi}");
                var content = new StringContent(JsonConvert.SerializeObject(dto), null, "application/json");
                httpRequestMessage.Content = content;
                var response = await client.SendAsync(httpRequestMessage);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic jsonResponse = JsonConvert.DeserializeObject(responseContent)!;
                    string token = jsonResponse.token.ToString();
                    //save to identity
                    return (Result: true, Token: token);
                }
                else
                {
                    return (Result: false, Token: "");
                }
            }
            catch
            {
                return (Result: false, Token: "");
            }
        }
    }
}
