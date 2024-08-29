using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("salary_worker")]
    public class SalaryWorkerView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }

        [Column("salary_id")]
        public Guid SalaryViewId { get; set; }
        public SalaryView SalaryView { get; set; }
    }
}
