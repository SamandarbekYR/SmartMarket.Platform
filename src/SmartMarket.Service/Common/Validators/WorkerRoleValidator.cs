using FluentValidation;
using SmartMarket.Service.DTOs.WorkerRole;

namespace SmartMarket.Service.Common.Validators;

public class WorkerRoleValidator : AbstractValidator<AddWorkerRoleDto>
{
    public WorkerRoleValidator()
    {
        RuleFor(wr => wr.RoleName)
            .NotEmpty()
            .WithMessage("Role name is required.");
    }
}
