using SmartMarket.Domain.Entities.Products;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Orders
{
    [Table("order_product")]
    public class OrderProduct : BaseEntity
    {
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Column("sales_request_id")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        [Column("count")]
        public int Count { get; set; }
        [Column("available_count")]
        public int AvailableCount { get; set; }
        [Column("item_total_cost")]
        public double ItemTotalCost { get; set; }
    }
}
