namespace SmartMarket.Service.DTOs.Products.ProductSale
{
    public class FilterProductSaleDto
    {
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? ProductName { get; set; }
        public string? WorkerName { get; set; }
        public int? Count { get; set; }
    }
}
