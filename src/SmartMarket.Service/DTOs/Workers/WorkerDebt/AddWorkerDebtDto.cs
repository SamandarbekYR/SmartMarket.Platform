namespace SmartMarket.Service.DTOs.Workers.WorkerDebt;

public class AddWorkerDebtDto
{
    public Guid WorkerId { get; set; }
    public double Amount { get; set; }
}
