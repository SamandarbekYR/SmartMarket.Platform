using SmartMarket.DataAccess.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Transactions
{
    [Table("transaction")]
    public class Transaction : BaseEntity
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

        public List<ProductSale> ProductSales { get; set; }
    }
}
