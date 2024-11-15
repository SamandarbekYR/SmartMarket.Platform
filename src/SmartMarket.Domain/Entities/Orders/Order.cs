using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.Products;
using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Orders
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Column("transaction_id")]
        public long TransactionId { get; set; }
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("partner_id")]
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public List<ProductSale> ProductSaleItems { get; set; }
    }
}   