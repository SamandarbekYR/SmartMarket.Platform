namespace SmartMarket.Service.DTOs.Products.DebtPayment;

public class DebtPaymentDto
{
    public Guid Id { get; set; }
    public Guid DebtorId { get; set; }
    public double DebtSum { get; set; }
    public double PayedSum { get; set; }
    public double RemainingSum { get; set; }
    public string PaymentType { get; set; } = string.Empty;
}
