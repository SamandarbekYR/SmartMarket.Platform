using FluentValidation;
using SmartMarket.Service.DTOs.Transaction;

namespace SmartMarket.Service.Common.Validators;

public class TransactionValidator : AbstractValidator<AddTransactionDto>
{
    public TransactionValidator()
    {
        RuleFor(t => t.Amount)
            .GreaterThan(0)
            .WithMessage("Transaction amount must be greater than zero.");

        RuleFor(t => t.From)
            .NotEmpty()
            .WithMessage("Transaction source is required.");

        RuleFor(t => t.To)
            .NotEmpty()
            .WithMessage("Transaction destination is required.");

        RuleFor(t => t.TypeOfPayment)
            .NotEmpty()
            .WithMessage("Type of payment is required.");
    }
}
