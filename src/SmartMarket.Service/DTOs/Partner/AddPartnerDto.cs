﻿namespace SmartMarket.Service.DTOs.Partner;

public class AddPartnerDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public double? LastPayment { get; set; }
    public double? TotalDebt { get; set; }
    public double? PaidDebt { get; set; }
    public DateTime LastPaymentDate { get; set; }
    public Guid? PartnerId { get; set; }
    public Guid? PayDeskId { get; set; }
    public string? PaymentType { get; set; }
}
