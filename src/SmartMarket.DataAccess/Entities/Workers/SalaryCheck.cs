using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("salary_check")]
    public class SalaryCheck : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }

        [Column("advance_check")]
        public bool AdvanceCheck {  get; set; }
        [Column("salary_check")]
        public bool Salarycheck {  get; set; }
        [Column("company_debt")]
        public double CompanyDebt { get; set; }
    }
}
