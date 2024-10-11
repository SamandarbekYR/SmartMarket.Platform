namespace SmartMarket.Service.DTOs.Products.ReplaceProduct
{
    public class FilterReplaceProductDto
    {
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? ProductName { get; set; }
        public string? WorkerName { get; set; }
        public int? Count { get; set; }
    }
}
