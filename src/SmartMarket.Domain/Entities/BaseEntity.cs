using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities
{
    public class BaseEntity
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("created_at")]
        public DateTime CreatedDate { get; set; }
    }
}
