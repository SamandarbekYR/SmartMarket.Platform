using SmartMarketDesktop.ViewModels.Entities.Partners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Products
{
    [Table("debtors")]
    public class DebttorsView : BaseEntity
    {

        [Column("partner_id")]
        public Guid PartnerId { get; set; }
        public PartnerView PartnerView { get; set; }
        [Column("product_id")]
        public Guid ProductId { get; set; }
        public ProductView ProductView { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("debt_sum")]
        public double DeptSum { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<DebtPaymentView> DebtPaymentViews { get; set; }
    }
}
