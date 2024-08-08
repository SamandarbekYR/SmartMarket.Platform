using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Workers
{
    [Table("salary")]
    public class Salary : BaseEntity
    {
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("amount")]
        public double Amount { get; set; }
        [Column("advance")]
        public double Advance {  get; set; }
        public List<SalaryWorker> SalaryWorkers { get; set;}
    }
}
