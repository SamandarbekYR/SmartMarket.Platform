using Microsoft.AspNetCore.Http;

namespace SmartMarket.Service.DTOs.Workers.Worker;

public class AddWorkerDto
{
    public Guid PositionId { get; set; }
    public Guid WorkerRoleId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
   // public IFormFile? ImgPath { get; set; }
    public string? Password { get; set; } = string.Empty;
}

