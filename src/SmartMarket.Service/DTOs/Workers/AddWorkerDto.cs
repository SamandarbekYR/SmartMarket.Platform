namespace SmartMarket.Service.DTOs.Workers;

public class AddWorkerDto
{
    public Guid PositionId { get; set; }
    public Guid WorkerRoleId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string ImgPath { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

