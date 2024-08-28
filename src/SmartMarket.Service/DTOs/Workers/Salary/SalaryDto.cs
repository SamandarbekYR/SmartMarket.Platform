namespace SmartMarket.Service.DTOs.Workers.Salary;

public class SalaryDto
{
    public Guid Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Amount { get; set; }
    public double Advance { get; set; }
}
