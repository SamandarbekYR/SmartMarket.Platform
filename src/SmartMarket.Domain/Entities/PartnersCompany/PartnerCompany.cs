using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Domain.Entities.PartnersCompany
{
    [Table("partners_company")]
    public class PartnerCompany : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; } = string.Empty;
        [Column("description")]
        public string Description { get; set; } = string.Empty;
        [Column("phone_number")]
        public string PhoneNumber { get; set; } = string.Empty;

        public List<ContrAgent> ContrAgents { get; set; }
    }
}
