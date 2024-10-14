using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;

using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Service.ViewModels.Products
{
    public class ProductSaleViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        public Guid TransactionId { get; set; }
        public Transaction Transaction { get; set; }
        public Guid PayDeskId { get; set; }
        public PayDesk PayDesk { get; set; }

        public long TransactionNumber { get; set; }
        public int Count { get; set; }
        public double Discount { get; set; }
        public double TotalCost { get; set; }
        public double CashSum { get; set; }
        public string CardSum { get; set; } 
        public double TransferMoney { get; set; }
        public double DebtSum { get; set; }
        public DateTime? CreatedDate { get; set; }

        public List<ReplaceProduct> ReplaceProducts { get; set; }
        public List<InvalidProduct> InvalidProducts { get; set; }
    }
}
