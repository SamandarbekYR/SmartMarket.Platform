﻿using Newtonsoft.Json;
using NLog;
using SmartMarket.Domain.Entities.Products;
using SmartMarketDeskop.Integrated.Api.Auth;
using SmartMarketDeskop.Integrated.Security;
using SmartMarketDeskop.Integrated.Server.Interfaces.Products;
using SmartMarketDesktop.DTOs.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDeskop.Integrated.Server.Repositories.Products
{
    public class ProductServer : IProductServer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        public async Task<bool> AddAsync(AddProductDto dto)
        {

            try
            {
                var token = IdentitySingelton.GetInstance().Token;
                var workerId=TokenHandler.ParseToken(token).Id;
                dto.WorkerId = workerId;

                using (var client = new HttpClient())
                using (var request = new HttpRequestMessage(HttpMethod.Post, AuthApi.BASE_URL + "/api/products"))
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

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(AddProductDto dto, Guid Id)
        {
            throw new NotImplementedException();
        }
    }
}