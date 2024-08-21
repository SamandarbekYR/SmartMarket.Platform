namespace SmartMarket.Service.DTOs.Expence;

public class AddExpenceDto
{
    public Guid WorkerId { get; set; }
    public Guid PayDeskId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string TypeOfPayment { get; set; } = string.Empty;
}
