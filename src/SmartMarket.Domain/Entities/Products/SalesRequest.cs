using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.Products
{
    [Table("sales_request")]
    public class SalesRequest : BaseEntity
    {
        [Column("transaction_id")]
        public long TransactionId { get; set; }
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskId { get; set; }
        public PayDesk PayDesk { get; set; }
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
        public List<ProductSale> ProductSaleItems { get; set; }
        
    }
}