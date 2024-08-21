using FluentValidation;
using SmartMarket.Service.DTOs.Order;

namespace SmartMarket.Service.Common.Validators;

public class OrderValidator : AbstractValidator<AddOrderDto>
{
    public OrderValidator()
    {
        RuleFor(o => o.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(o => o.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(o => o.TransactionNumber)
            .NotEmpty()
            .WithMessage("Transaction number is required.");

        RuleFor(o => o.Count)
            .GreaterThan(0)
            .WithMessage("Order count must be greater than zero.");
    }
}
