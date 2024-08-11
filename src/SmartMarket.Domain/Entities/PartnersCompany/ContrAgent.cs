using SmartMarket.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Domain.Entities.PartnersCompany
{
    [Table("contr_agent")]
    public class ContrAgent : BaseEntity
    {
        [Column("company_id")]
        public Guid CompanyId { get; set; }
        public PartnerCompany PartnerCompany { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber {  get; set; } = string.Empty;

        public List<Product> Products { get; set; }
        public List<LoadReport> LoadReports { get; set; }
    }
}
