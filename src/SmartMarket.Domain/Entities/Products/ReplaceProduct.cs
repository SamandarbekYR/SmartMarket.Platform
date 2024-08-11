using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Products
{
    [Table("replace_product")]
    public class ReplaceProduct : BaseEntity
    {
        [Column("product_sale_id")]
        public Guid ProductSaleId { get; set; }
        public ProductSale ProductSale { get; set; }
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("reason")]
        public string Reason { get; set; } = string.Empty;
    }
}
