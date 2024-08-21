namespace SmartMarket.Service.DTOs.Customer;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string PhoneNumber2 { get; set; } = string.Empty;
    public string Adress { get; set; } = string.Empty;
}
