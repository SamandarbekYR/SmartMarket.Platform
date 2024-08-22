namespace SmartMarket.Service.DTOs.ProductImage;

public class AddProductImageDto
{
    public Guid ProductId { get; set; }
    public string ImagePath { get; set; } = string.Empty;
}
