using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.Orders
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        [Column("transaction_number")]
        public string TransactionNumber { get; set; } = string.Empty;
        [Column("count")]
        public int Count { get; set; }
    }
}
