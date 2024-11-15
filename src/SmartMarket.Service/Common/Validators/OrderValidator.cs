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

        RuleFor(o => o.PartnerId)
            .NotEmpty()
            .WithMessage("Partner ID is required.");
    }
}
