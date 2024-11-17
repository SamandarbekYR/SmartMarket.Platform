namespace SmartMarketDeskop.Integrated.Security;

public class IdentitySingelton
{
    public static IdentitySingelton _identitySingelton;
    public string Token { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public Guid PayDeskId { get; set; }
    public string PayDeskName { get; set; } = string.Empty;
    public string PrinterName { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public static IdentitySingelton GetInstance()
    {
        if (_identitySingelton == null)
        {
            _identitySingelton = new IdentitySingelton();
        }

        return _identitySingelton;
    }

    public void Reset()
    {
        Token = string.Empty;
        Id = Guid.Empty;
        PayDeskId = Guid.Empty;
        PayDeskName = string.Empty;
        PrinterName = string.Empty;
        RoleName = string.Empty;
        FirstName = string.Empty;
        LastName = string.Empty;
        PhoneNumber = string.Empty;
    }
}
