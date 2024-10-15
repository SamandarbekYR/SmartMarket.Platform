using Newtonsoft.Json;
using NLog;
using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Workers;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Workers
{
    public class PositionServer : IPositionServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<bool> AddAsync(AddPositionDto dto)
        {
            try
            {
                var token = IdentitySingelton.GetInstance().Token;

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/super-admin/positions"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");

                    var json = JsonConvert.SerializeObject(dto);

                    var content = new StringContent(json, Encoding.UTF8, "application/json");

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
                        logger.Error($"Failed to add Contr Agent. Status Code: {response.StatusCode}, Response: {resultContent}");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ContrAgentServer: AddAsync method exception: ", ex);
                return false;
            }
        }

        public Task<bool> DeleteAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PositionDto>> GetAllAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                var token = IdentitySingelton.GetInstance().Token;
                client.BaseAddress = new Uri($"{AuthApi.BASE_URL}/api/super-admin/positions");

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage message = await client.GetAsync(client.BaseAddress);

                string response = await message.Content.ReadAsStringAsync();

                List<PositionDto> positions = JsonConvert.DeserializeObject<List<PositionDto>>(response)!;

                return positions;

            }
            catch
            {
                return new List<PositionDto>();
            }
        }

        public async Task<bool> UpdateAsync(AddPositionDto dto, Guid Id)
        {
            try 
            {                 
                var token = IdentitySingelton.GetInstance().Token;
            
                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Put, AuthApi.BASE_URL + $"/api/super-admin/positions/{Id}"))
                {
                    request.Headers.Add("Authorization", $"Bearer {token}");
                    var json = JsonConvert.SerializeObject(dto);
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
                        logger.Error($"Failed to add Contr Agent. Status Code: {response.StatusCode}, Response: {resultContent}");
                    }

                    return false;
                }
            }
            catch (Exception ex)
            {
                logger.Error("ContrAgentServer: AddAsync method exception: ", ex);
                return false;
            }
        }
    }
}
