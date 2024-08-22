using FluentValidation;
using SmartMarket.Service.DTOs.WorkerDebt;

namespace SmartMarket.Service.Common.Validators;

public class WorkerDebtValidator : AbstractValidator<AddWorkerDebtDto>
{
    public WorkerDebtValidator()
    {
        RuleFor(wd => wd.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(wd => wd.Amount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Debt amount must be greater than or equal to zero.");
    }
}
