using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.Partners
{
    [Table("partner")]
    public class PartnerView : BaseEntity
    {
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("total_debt")]
        public double TotalDebt { get; set; }
        [Column("last_payment")]
        public DateTime LastPayment { get; set; }
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<DebttorsView> DebttorsViews { get; set; }
    }
}
