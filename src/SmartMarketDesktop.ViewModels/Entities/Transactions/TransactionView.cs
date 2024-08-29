using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Transactions
{
    [Table("transaction")]
    public class TransactionView : BaseEntity
    {

        [Column("amount")]
        public double Amount { get; set; }
        [Column("from")]
        public string From { get; set; } = string.Empty;
        [Column("to")]
        public string To { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("type_of_payment")]
        public string TypeOfPayment { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<ProductSaleView> ProductSaleViews { get; set; }
    }
}
