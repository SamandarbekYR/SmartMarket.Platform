using FluentValidation;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;

namespace SmartMarket.Service.Common.Validators;

public class SalaryWorkerValidator : AbstractValidator<AddSalaryWorkerDto>
{
    public SalaryWorkerValidator()
    {
        RuleFor(sw => sw.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(sw => sw.SalaryId)
            .NotEmpty()
            .WithMessage("Salary ID is required.");
    }
}
