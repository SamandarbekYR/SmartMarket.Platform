namespace SmartMarket.Service.DTOs.DebtPayment;

public class AddDebtPaymentDto
{
    public Guid DebtorId { get; set; }
    public double DebtSum { get; set; }
    public double PayedSum { get; set; }
    public string PaymentType { get; set; } = string.Empty;
}
