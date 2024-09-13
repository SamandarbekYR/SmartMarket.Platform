namespace SmartMarketDesktop.DTOs.DTOs;

public class PartnerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double TotalDebt { get; set; }
    public DateTime LastPayment { get; set; }

}
