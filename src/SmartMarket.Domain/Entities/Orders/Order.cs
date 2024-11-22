using SmartMarket.Domain.Entities.Partners;
using SmartMarket.Domain.Entities.Workers;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Orders
{
    [Table("order")]
    public class Order : BaseEntity
    {
        [Column("is_sold")]
        public bool IsSold { get; set; } = false;
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("partner_id")]
        public Guid PartnerId { get; set; }
        public Partner Partner { get; set; }
        public List<OrderProduct> ProductOrderItems { get; set; }
    }
}   