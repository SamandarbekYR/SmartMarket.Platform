namespace SmartMarket.Service.DTOs.Workers.Worker;

public class WorkerDto
{
    public Guid Id { get; set; }
    public Guid PositionId { get; set; }
    public Guid WorkerRoleId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
}