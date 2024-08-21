using FluentValidation;
using SmartMarket.Service.DTOs.Customer;

namespace SmartMarket.Service.Common.Validators;

public class CustomerValidator : AbstractValidator<AddCustomerDto>
{
    public CustomerValidator()
    {
        RuleFor(c => c.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(c => c.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");

        // RuleFor(c => c.PhoneNumber)
        //     .Matches(@"^\d{2}-\d{3}-\d{4}$")
        //     .WithMessage("Phone number must be in the format XX-XXX-XXXX.");
    }
}
