using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Workers
{
    [Table("worker_debt")]
    public class WorkerDebt : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("amount")]
        public double Amount { get; set; }
    }
}
