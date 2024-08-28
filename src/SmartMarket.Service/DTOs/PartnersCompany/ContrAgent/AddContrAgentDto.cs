namespace SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;

public class AddContrAgentDto
{
    public Guid CompanyId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
