namespace SmartMarket.Service.DTOs.WorkerDebt;

public class WorkerDebtDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public double Amount { get; set; }
}
