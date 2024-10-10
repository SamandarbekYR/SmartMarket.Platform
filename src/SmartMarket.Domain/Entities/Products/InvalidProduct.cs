using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

    namespace SmartMarket.Domain.Entities.Products
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
            [Column("count")]
            public int Count { get; set; }
            [Column("return_reason")]
            public string ReturnReason { get; set; } = string.Empty;
        }
    }
