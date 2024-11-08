using SmartMarket.Domain.Entities.PayDesks;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.PartnersCompany
{
    [Table("contr_agent_payment")]
    public class ContrAgentPayment : BaseEntity
    {
        [Column("contr_agent_id")]
        public Guid ContrAgentId { get; set; }
        public ContrAgent ContrAgent { get; set; }
        [Column("pay_desk_id")]
        public Guid PayDeskId { get; set; }
        public PayDesk PayDesk { get; set; }
        [Column("payment_type")]
        public string PaymentType { get; set; }

        [Column("total_debt")]
        public double TotalDebt { get; set; }
        [Column("total_payment")]
        public double LastPayment { get; set; }
        [Column("last_payment_date")]
        public DateTime LastPaymentDate { get; set; }
    }
}
