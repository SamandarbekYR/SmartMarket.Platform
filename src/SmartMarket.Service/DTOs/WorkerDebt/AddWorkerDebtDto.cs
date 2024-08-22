namespace SmartMarket.Service.DTOs.WorkerDebt;

public class AddWorkerDebtDto
{
    public Guid WorkerId { get; set; }
    public double Amount { get; set; }
}
