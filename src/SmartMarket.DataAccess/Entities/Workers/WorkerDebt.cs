using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("worker_debt")]
    public class WorkerDebt : BaseEntity
    {
        [Column("amount")]
        public double Amount { get; set; }

        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
    }
}
