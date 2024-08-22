using FluentValidation;
using SmartMarket.Service.DTOs.ProductSale;

namespace SmartMarket.Service.Common.Validators;

public class ProductSaleValidator : AbstractValidator<AddProductSaleDto>
{
    public ProductSaleValidator()
    {
        RuleFor(ps => ps.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(ps => ps.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(ps => ps.TransactionId)
            .NotEmpty()
            .WithMessage("Transaction ID is required.");

        RuleFor(ps => ps.PayDeskId)
            .NotEmpty()
            .WithMessage("Pay Desk ID is required.");

        RuleFor(ps => ps.TransactionNumber)
            .NotEmpty()
            .WithMessage("Transaction number is required.");

        RuleFor(ps => ps.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product count must be greater than or equal to zero.");

        RuleFor(ps => ps.TotalCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total cost must be greater than or equal to zero.");

        RuleFor(ps => ps.CashSum)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Cash amount must be greater than or equal to zero.");

        RuleFor(ps => ps.TransferMoney)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Transfer amount must be greater than or equal to zero.");

        RuleFor(ps => ps.DebtSum)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debt amount must be greater than or equal to zero.");
    }
}