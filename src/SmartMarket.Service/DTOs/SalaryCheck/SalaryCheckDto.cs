namespace SmartMarket.Service.DTOs.SalaryCheck;

public class SalaryCheckDto
{
    public Guid Id { get; set; }
    public Guid WorkerId { get; set; }
    public bool AdvanceCheck { get; set; }
    public bool SalaryCheck { get; set; }
    public double CompanyDebt { get; set; }
}
