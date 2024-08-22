namespace SmartMarket.Service.DTOs.Transaction;

public class TransactionDto
{
    public Guid Id { get; set; }
    public double Amount { get; set; }
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TypeOfPayment { get; set; } = string.Empty;
}
