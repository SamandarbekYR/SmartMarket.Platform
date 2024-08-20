using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.Services.Category;

namespace SmartMarket.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(CategoryService categoryService) : ControllerBase
{
    private readonly CategoryService _categoryService = categoryService;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CategoryDto dto)
    {
        await _categoryService.AddAsync(dto);
        return Ok();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] CategoryDto dto)
    {
        await _categoryService.UpdateAsync(dto, id);
        return Ok();
    }
}
