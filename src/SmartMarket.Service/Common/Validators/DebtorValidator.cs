using FluentValidation;
using SmartMarket.Service.DTOs.Debtors;

namespace SmartMarket.Service.Common.Validators;

public class DebtorValidator : AbstractValidator<AddDebtorsDto>
{
    public DebtorValidator()
    {
        RuleFor(d => d.PartnerId)
            .NotEmpty()
            .WithMessage("Partner ID is required.");

        RuleFor(d => d.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(d => d.DeptSum)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debt amount must be greater than or equal to zero.");
    }
}
