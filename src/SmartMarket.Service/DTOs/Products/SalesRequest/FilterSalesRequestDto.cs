namespace SmartMarket.Service.DTOs.Products.SalesRequest
{
    public class FilterSalesRequestDto
    {
        public Guid? PayDeskId { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
        public string? ProductName { get; set; }
        public string? WorkerName { get; set; }
    }
}
