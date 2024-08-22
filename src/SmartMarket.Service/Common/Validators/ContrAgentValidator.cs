using FluentValidation;
using SmartMarket.Service.DTOs.ContrAgent;

namespace SmartMarket.Service.Common.Validators;

public class ContrAgentValidator : AbstractValidator<AddContrAgentDto>
{
    public ContrAgentValidator()
    {
        RuleFor(ca => ca.CompanyId)
            .NotEmpty()
            .WithMessage("Company ID is required.");

        RuleFor(ca => ca.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(ca => ca.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(ca => ca.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");
    }
}
