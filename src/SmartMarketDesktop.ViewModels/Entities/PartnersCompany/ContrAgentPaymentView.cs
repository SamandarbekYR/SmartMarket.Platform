using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.PartnersCompany
{
    [Table("contr_agent_payment")]
    public class ContrAgentPaymentView : BaseEntity
    {

        [Column("contr_agent_id")]
        public Guid ContrAgentId { get; set; }
        public ContrAgentView ContrAgentView { get; set; }

        [Column("total_debt")]
        public double TotalDebt { get; set; }
        [Column("total_payment")]
        public double LastPayment { get; set; }
        [Column("last_payment_date")]
        public DateTime LastPaymentDate { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
