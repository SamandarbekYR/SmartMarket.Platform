using FluentValidation;
using SmartMarket.Service.DTOs.Workers;

namespace SmartMarket.Service.Common.Validators;

public class WorkerValidator : AbstractValidator<AddWrokerDto>
{
    public WorkerValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");

        RuleFor(x => x.PositionId)
            .NotEmpty()
            .WithMessage("Position ID is required.");

        RuleFor(x => x.WorkerRoleId)
            .NotEmpty()
            .WithMessage("Worker Role ID is required.");
    }
}