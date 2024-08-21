using FluentValidation;
using SmartMarket.Service.DTOs.Expence;

namespace SmartMarket.Service.Common.Validators;

public class ExpenceValidator : AbstractValidator<AddExpenceDto>
{
    public ExpenceValidator()
    {
        RuleFor(e => e.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(e => e.PayDeskId)
            .NotEmpty()
            .WithMessage("Pay Desk ID is required.");

        RuleFor(e => e.Reason)
            .NotEmpty()
            .WithMessage("Reason for expense is required.");

        RuleFor(e => e.Amount)
            .GreaterThan(0)
            .WithMessage("Expense amount must be greater than zero.");

        RuleFor(e => e.TypeOfPayment)
            .NotEmpty()
            .WithMessage("Type of payment is required.");
    }
}
