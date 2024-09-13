using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarketDesktop.ViewModels.Entities.PartnersCompany;

[Table("partners_company")]
public class PartnerCompanyView : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    [Column("description")]
    public string Description { get; set; } = string.Empty;
    [Column("phone_number")]
    public string PhoneNumber { get; set; } = string.Empty;
    [Column("issynced")]
    public bool IsSynced { get; set; }

    public List<ContrAgentView> ContrAgentViews { get; set; }
}
