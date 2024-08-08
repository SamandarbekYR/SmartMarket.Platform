using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("salary_worker")]
    public class SalaryWorker : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("salary_id")]
        public Guid SalaryId { get; set; }
        public Salary Salary { get; set; }
    }
}
