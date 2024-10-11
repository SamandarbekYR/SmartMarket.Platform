namespace SmartMarket.Service.DTOs.Products.InvalidProduct
{
    public class FilterInvalidProductDto
    {
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? ProductName { get; set; }
        public string? WorkerName { get; set; }
    }
}
