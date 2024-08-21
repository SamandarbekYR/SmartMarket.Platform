namespace SmartMarket.Service.DTOs.PayDesks;

public class PayDesksDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Income { get; set; }
    public double Outlay { get; set; }
}
