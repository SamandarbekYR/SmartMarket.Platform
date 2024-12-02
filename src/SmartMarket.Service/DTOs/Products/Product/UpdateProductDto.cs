namespace SmartMarket.Service.DTOs.Products.Product
{
    public class UpdateProductDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public bool IsIncrement { get; set; }
    }
}
