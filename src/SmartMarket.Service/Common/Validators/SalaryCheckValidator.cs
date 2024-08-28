using FluentValidation;
using SmartMarket.Service.DTOs.Workers.SalaryCheck;

namespace SmartMarket.Service.Common.Validators;

public class SalaryCheckValidator : AbstractValidator<AddSalaryCheckDto>
{
    public SalaryCheckValidator()
    {
        RuleFor(s => s.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(s => s.CompanyDebt)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Company debt must be greater than or equal to zero.");
    }
}