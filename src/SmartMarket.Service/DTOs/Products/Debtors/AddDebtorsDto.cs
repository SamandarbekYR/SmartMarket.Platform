﻿namespace SmartMarket.Service.DTOs.Products.Debtors;

public class AddDebtorsDto
{
    public Guid PartnerId { get; set; }
    public Guid ProductId { get; set; }
    public string Description { get; set; } = string.Empty;
    public double DeptSum { get; set; }
}