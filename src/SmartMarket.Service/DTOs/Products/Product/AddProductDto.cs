namespace SmartMarket.Service.DTOs.Products.Product;

public class AddProductDto
{
    public Guid CategoryId { get; set; }
    public Guid ContrAgentId { get; set; }
    public Guid WorkerId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string PCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Price { get; set; }
    public double SellPrice { get; set; }
    public int SellPricePersentage { get; set; }
    public string UnitOfMeasure { get; set; } = string.Empty;
    public string PaymentStatus { get; set; } = string.Empty;
}
