using SmartMarket.DataAccess.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Products
{
    [Table("invalid_product")]
    public class InvalidProduct : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("product_sale_id")]
        public Guid ProductSaleId { get; set; }
        public ProductSale ProductSale { get; set; }
        [Column("return_reason")]
        public string ReturnReason { get; set; } = string.Empty;
    }
}
