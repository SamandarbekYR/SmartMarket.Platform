using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.Workers
{
    [Table("salary")]
    public class Salary : BaseEntity
    {
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("amount")]
        public double Amount { get; set; }
        [Column("advance")]
        public double Advance { get; set; }
        public List<SalaryWorker> SalaryWorkers { get; set; }
    }
}
