﻿using SmartMarket.Service.DTOs.Order;

using SmartMarketDeskop.Integrated.Server.Interfaces.Orders;
using SmartMarketDeskop.Integrated.Server.Repositories.Orders;

using System.Net;

namespace SmartMarketDeskop.Integrated.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderServer _orderServer;

        public OrderService()
        {
            _orderServer = new OrderServer();
        }

        public async Task<bool> CreateAsync(AddOrderDto order)
        {
            if (IsInternetAvialable())
            {
                bool result = await _orderServer.CreateAsync(order);
                if (result)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            if (IsInternetAvialable())
            {
                var orders = await _orderServer.GetAllAsyc();
                return orders;
            }
            else
            {
                return new List<OrderDto>();
            }
        }

        public bool IsInternetAvialable()
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
    }
}