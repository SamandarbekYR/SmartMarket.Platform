﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.Interfaces.Order;

namespace SmartMarket.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddOrderDto dto)
    {
        await _orderService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _orderService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AddOrderDto dto)
    {
        await _orderService.UpdateAsync(dto, id);
        return Ok();
    }
}