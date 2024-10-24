using SmartMarket.Service.DTOs.Products.ProductSale;
namespace SmartMarket.Service.DTOs.Products.SalesRequest
{
    public class AddSalesRequestDto
    {
        public Guid WorkerId { get; set; }
        public Guid PayDeskId { get; set; }
        public double? TotalCost { get; set; }
        public double? CashSum { get; set; }
        public double? CardSum { get; set; } 
        public double? DebtSum { get; set; }
        public double? DiscountSum { get; set; }
        public List<AddProductSaleDto> ProductSaleItems { get; set; }
    }
}
