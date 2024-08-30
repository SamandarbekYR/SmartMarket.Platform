using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using SmartMarketDesktop.DTOs.DTOs.Auth;
using SmartMarketDeskop.Integrated.Interfaces.Auth;
using System.Text.Json;
using System.Text;
using SmartMarketDeskop.Integrated.API;
using SmartMarketDesktop.DTOs.DTOs.Worker;

namespace SmartMarketDeskop.Integrated.Services.Auth;

public class AuthService : IAuthService
{
    private readonly string _apiUrl;

    public AuthService()
    {
        _apiUrl = AuthAPI.BASE_URL;
    }

    public async Task<(bool Result, string Token)> LoginAsync(WorkerLoginDto dto)
    {
        try
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync($"{_apiUrl}/auth/login",
                new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var loginResult = JsonSerializer.Deserialize<LoginResponseDto>(jsonResponse);

                return (Result: true, Token: loginResult.Token);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return (Result: false, Token: string.Empty);
            }
        }
        catch (Exception ex)
        {
            return (Result: false, Token: string.Empty);
        }
    }
}
