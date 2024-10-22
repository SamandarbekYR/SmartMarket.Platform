using SmartMarket.Domain.Entities.Workers;
using SmartMarket.Service.DTOs.Products.ProductSale;

using PS = SmartMarket.Domain.Entities.Products;
namespace SmartMarket.Service.DTOs.Products.SalesRequest
{
    public class AddSalesRequestDto
    {
        public Guid WorkerId { get; set; }
        public Guid PayDeskId { get; set; }
        public double? TotalCost { get; set; }
        public double? CashSum { get; set; }
        public string CardSum { get; set; } = string.Empty;
        public double? DebtSum { get; set; }
        public List<AddProductSaleDto> ProductSaleItems { get; set; }
    }
}
