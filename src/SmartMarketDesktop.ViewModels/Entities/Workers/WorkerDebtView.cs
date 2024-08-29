using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("worker_debt")]
    public class WorkerDebtView : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerViewId { get; set; }
        public WorkerView WorkerView { get; set; }

        [Column("amount")]
        public double Amount { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
