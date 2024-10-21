using SmartMarket.Domain.Entities.Products;
using PC = SmartMarket.Domain.Entities.PartnersCompany;
namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;

public class ContrAgentDto
{ 
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public Guid CompanyId { get; set; }
    public PC.PartnerCompany PartnerCompany { get; set; }
    public List<Product> Products { get; set; }
    public List<LoadReport> LoadReports { get; set; }
    public List<PC.ContrAgentPayment> ContrAgentPayment { get; set; }
}
