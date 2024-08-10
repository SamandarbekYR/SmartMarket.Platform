using SmartMarket.DataAccess.Entities.PayDesks;
using SmartMarket.DataAccess.Entities.Workers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.DataAccess.Entities.Expenses
{
    [Table("expense")]
    public class Expense : BaseEntity
    {
        [Column("worker_id")]
        public Guid WorkerId { get; set; }
        public Worker Worker { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskId { get; set; }
        public PayDesk PayDesk { get; set; }

        [Column("reason")]
        public string Reason { get; set; } = string.Empty;
        [Column("amount")]
        public double Amount { get; set; }
        [Column("type_of_payment")]
        public string TypeOfPayment { get; set; } = string.Empty;
    }
}
