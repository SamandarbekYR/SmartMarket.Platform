using SmartMarket.Domain.Entities.PayDesks;
using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;
using Tr = SmartMarket.Domain.Entities.Transactions;

namespace SmartMarket.Domain.Entities.Products
{
    [Table("product_sale")]
    public class ProductSale : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("sales_request_id")]
        public Guid SalesRequestId { get; set; }
        public SalesRequest SalesRequest { get; set; }
        [Column("count")]
        public int Count { get; set; }
        [Column("discount")]
        public double Discount { get; set; }
        [Column("item_total_cost")]
        public double ItemTotalCost { get; set; }
        [Column("cash_sum")]

        public List<ReplaceProduct> ReplaceProducts { get; set; }
        public List<InvalidProduct> InvalidProducts { get; set; }
    }   
}
