using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("salary_check")]
    public class SalaryCheckView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }

        [Column("advance_check")]
        public bool AdvanceCheck { get; set; }
        [Column("salary_check")]
        public bool Salarycheck { get; set; }
        [Column("company_debt")]
        public double CompanyDebt { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
