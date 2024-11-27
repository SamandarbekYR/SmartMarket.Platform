using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarket.Service.DTOs.Partner;

public class AddPartnerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double? TotalDebt { get; set; }
    public DateTime? LastPayment { get; set; }
}
