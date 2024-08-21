using FluentValidation;
using SmartMarket.Service.DTOs.PayDesks;

namespace SmartMarket.Service.Common.Validators;

public class PayDeskValidator : AbstractValidator<AddPayDesksDto>
{
    public PayDeskValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Pay desk name is required.");
    }
}
