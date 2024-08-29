using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Workers
{
    [Table("salary")]
    public class SalaryView : BaseEntity
    {
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("amount")]
        public double Amount { get; set; }
        [Column("advance")]
        public double Advance { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
        public List<SalaryWorkerView> SalaryWorkerViews { get; set; }
    }
}
