using SmartMarket.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Transactions
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
    }
}
