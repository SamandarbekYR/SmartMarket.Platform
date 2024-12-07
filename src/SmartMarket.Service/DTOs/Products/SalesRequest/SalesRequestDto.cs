using P = SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Workers;
using PS = SmartMarket.Domain.Entities.Products;    
namespace SmartMarket.Service.DTOs.Products.SalesRequest
{
    public class SalesRequestDto
    {
        public long TransactionId { get; set; }
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        public Guid PartnerId { get; set; }
        public P.Partner Partner { get; set; }
        public Guid? PayDeskId { get; set; }
        public PayDesk? PayDesk { get; set; }
        public double TotalCost { get; set; }
        public double CashSum { get; set; }
        public double CardSum { get; set; } 
        public double TransferMoney { get; set; }
        public double DebtSum { get; set; }
        public bool IsShipment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public List<PS.ProductSale> ProductSaleItems { get; set; }
    }
}
