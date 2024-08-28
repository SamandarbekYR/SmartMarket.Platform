using Microsoft.AspNetCore.Http;

namespace SmartMarket.Service.DTOs.Products.ProductImage;

public class AddProductImageDto
{
    public Guid ProductId { get; set; }
    public IFormFile ImagePath { get; set; } 
}
