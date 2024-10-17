using Newtonsoft.Json;
using NLog;
using SmartMarket.Service.DTOs.Expence;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Expenses;
using System.Text;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Expenses;


public class ExpensesServer : IExpensesServer
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();

    public async Task<bool> CreateAsync(AddExpenceDto expence)
    {
        try
        {
            var token = IdentitySingelton.GetInstance().Token;
            var workerId = TokenHandler.ParseToken(token).Id;
            expence.WorkerId = workerId;

            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/expences"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(expence);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    request.Content = content;

                    var response = await client.SendAsync(request);

                    var resultContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        logger.Error($"Failed to add Expense. Status Code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {resultContent}");
                    }

                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            logger.Error("ExpensesServer: CreateAsync method exception: ", ex);
            return false;
        }
    }

    public async Task<List<FullExpenceDto>> FilterExpenceAsync(FilterExpenseDto filterExpenceDto)
    {
        try
        {
            HttpClient client = new HttpClient();
            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/expences/filter");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(filterExpenceDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage message = await client.PostAsync(client.BaseAddress, data);
            string response = await message.Content.ReadAsStringAsync();

            List<FullExpenceDto> expences = JsonConvert.DeserializeObject<List<FullExpenceDto>>(response)!;

            return expences;
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            return new List<FullExpenceDto>();
        }
    }

    public async Task<List<FullExpenceDto>> GetExpensesFullInformationAsync()
    {
        try
        {
            HttpClient client = new HttpClient();

            var token = IdentitySingelton.GetInstance().Token;
            client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/expences");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = await client.GetAsync(client.BaseAddress);

            string response = await responseMessage.Content.ReadAsStringAsync();
            List<FullExpenceDto> expenses = JsonConvert.DeserializeObject<List<FullExpenceDto>>(response)!;

            return expenses;
        }
        catch (Exception ex)
        {
            return new List<FullExpenceDto>();
        }
    }

}
