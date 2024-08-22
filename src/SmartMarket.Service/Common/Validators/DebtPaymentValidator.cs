using FluentValidation;
using SmartMarket.Service.DTOs.DebtPayment;

namespace SmartMarket.Service.Common.Validators;

public class DebtPaymentValidator : AbstractValidator<AddDebtPaymentDto>
{
    public DebtPaymentValidator()
    {
        RuleFor(dp => dp.DebtorId)
            .NotEmpty()
            .WithMessage("Debtor ID is required.");

        RuleFor(dp => dp.DebtSum)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debt amount must be greater than or equal to zero.");

        RuleFor(dp => dp.PayedSum)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Paid amount must be greater than or equal to zero.");

        RuleFor(dp => dp.PaymentType)
            .NotEmpty()
            .WithMessage("Payment type is required.");
    }
}
