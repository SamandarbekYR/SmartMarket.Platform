namespace SmartMarket.Service.DTOs.Order
{
    public class AddOrderProductDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public int AvailableCount { get; set; }
        public double ItemTotalCost { get; set; }
    }
}
