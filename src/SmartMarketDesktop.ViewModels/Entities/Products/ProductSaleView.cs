using SmartMarketDesktop.ViewModels.Entities.PayDesk;
using SmartMarketDesktop.ViewModels.Entities.Transactions;
using SmartMarketDesktop.ViewModels.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("product_sale")]
    public class ProductSaleView : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductViewId { get; set; }
        public ProductView ProductView { get; set; }
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }
        [Column("transaction_id")]
        public Guid TransactionViewId { get; set; }
        public TransactionView TransactionView { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskViewId { get; set; }
        public PayDeskView PayDeskView { get; set; }

        [Column("transaction_number")]
        public long TransactionNumber { get; set; }
        [Column("count")]
        public int Count { get; set; }
        [Column("total_cost")]
        public double TotalCost { get; set; }
        [Column("cash_sum")]
        public double CashSum { get; set; }
        [Column("card_sum")]
        public string CardSum { get; set; } = string.Empty;
        [Column("transfer_money")]
        public double TransferMoney { get; set; }
        [Column("debt_sum")]
        public double DebtSum { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<ReplaceProductView> ReplaceProductViews { get; set; }
        public List<InValidProductView> InValidProductViews { get; set; }
    }
}
