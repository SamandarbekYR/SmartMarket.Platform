using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("debt_payment")]
    public class DebtPaymentView : BaseEntity
    {
        [Column("debtor_id")]
        public Guid DebtorId { get; set; }
        public DebttorsView DebttorsView { get; set; }

        [Column("debt_sum")]
        public double DebtSum { get; set; }
        [Column("payed_sum")]
        public double PayedSum { get; set; }
        [Column("remaining_sum")]
        public double RemainingSum { get; set; }
        [Column("payment_type")]
        public string PaymentType { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }
    }
}
