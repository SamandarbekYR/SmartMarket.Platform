using SmartMarket.Domain.Entities.Products;

namespace SmartMarket.Service.DTOs.Partner;

public class PartnerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double TotalDebt { get; set; }
    public DateTime LastPayment { get; set; }
    public string PaymentType { get; set; } = string.Empty;
    public List<Debtors> Debtors { get; set; }
}
