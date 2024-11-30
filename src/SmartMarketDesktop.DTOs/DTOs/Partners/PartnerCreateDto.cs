namespace SmartMarketDesktop.DTOs.DTOs.Partners;

public class PartnerCreateDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double? LastPayment { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public string PaymentType { get; set; }
}
