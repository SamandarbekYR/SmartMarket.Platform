using FluentValidation;
using SmartMarket.Service.DTOs.Partner;

namespace SmartMarket.Service.Common.Validators;

public class PartnerValidator : AbstractValidator<AddPartnerDto>
{
    public PartnerValidator()
    {
        RuleFor(p => p.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(p => p.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(p => p.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");
    }
}
