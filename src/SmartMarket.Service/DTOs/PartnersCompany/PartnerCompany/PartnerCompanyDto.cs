﻿namespace SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;

public class PartnerCompanyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}