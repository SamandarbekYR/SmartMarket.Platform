namespace SmartMarket.Service.DTOs.Workers.WorkerDebt;

public class WorkerDebtDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public double Amount { get; set; }
}
