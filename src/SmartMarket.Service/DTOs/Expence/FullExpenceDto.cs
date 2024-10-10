namespace SmartMarket.Service.DTOs.Expence;

public class FullExpenceDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public Guid PayDeskId { get; set; }
    public string Reason { get; set; } = string.Empty;
    public double Amount { get; set; }
    public string TypeOfPayment { get; set; } = string.Empty;
    public string PayDeskName { get; set; } = string.Empty;
    public string WorkerFirsName { get; set; } = string.Empty;
    public string WorkerLastName { get; set; } = string.Empty;
}
