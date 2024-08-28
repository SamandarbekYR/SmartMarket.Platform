namespace SmartMarket.Service.DTOs.Workers.SalaryCheck;

public class AddSalaryCheckDto
{
    public Guid WorkerId { get; set; }
    public bool AdvanceCheck { get; set; }
    public bool SalaryCheck { get; set; }
    public double CompanyDebt { get; set; }
}
