using SmartMarket.Domain.Entities.Categories;
using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;

namespace SmartMarket.Service.ViewModels.Products
{
    public class ProductSaleViewModel
    {
        public long TransactionNumber { get; set; }
        public string ProductName { get; set; }
        public string BarCode { get; set; }
        public string CategoryName { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public double TotalCost { get; set; }
        public string WorkerName { get; set; }
        public double CashSum { get; set; }
        public string CardSum { get; set; }
        public double TransferMoney { get; set; }
        public double DebtSum { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
