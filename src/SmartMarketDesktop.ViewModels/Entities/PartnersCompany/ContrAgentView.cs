using SmartMarketDesktop.ViewModels.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarketDesktop.ViewModels.Entities.PartnersCompany
{
    [Table("contr_agent")]
    public class ContrAgentView : BaseEntity
    {
        [Column("company_id")]
        public Guid CompanyId { get; set; }
        public PartnerCompanyView PartnerCompanyView { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Column("issynced")]
        public bool IsSynced { get; set; }

        public List<ProductView> ProductViews { get; set; }
        public List<LoadReportView> LoadReportViews { get; set; }
        public List<ContrAgentPaymentView> ContrAgentPaymentViews { get; set; }
    }
}
