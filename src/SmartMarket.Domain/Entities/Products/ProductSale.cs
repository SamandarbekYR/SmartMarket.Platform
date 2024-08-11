using Tr=SmartMarket.Domain.Entities.Transactions;
using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using SmartMarket.Domain.Entities.PayDesks;

namespace SmartMarket.Domain.Entities.Products
{
    [Table("product_sale")]
    public class ProductSale : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("worker_id")]
        public Guid WorkerId {  get; set; }
        public Worker Worker { get; set; }
        [Column("transaction_id")]
        public Guid TransactionId { get; set; }
        public Tr.Transaction Transaction { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskId { get; set; }
        public PayDesk PayDesk { get; set; }

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
        public double DebtSum {  get; set; }

        public List<ReplaceProduct> ReplaceProducts { get; set; }
        public List<InvalidProduct> InvalidProducts { get; set; }
    }   
}
