using FluentValidation;
using SmartMarket.Service.DTOs.Salary;

namespace SmartMarket.Service.Common.Validators
{
    public class SalaryValidator : AbstractValidator<AddSalaryDto>
    {
        public SalaryValidator()
        {
            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("Salary description is required.");

            RuleFor(s => s.Amount)
                .GreaterThan(0)
                .WithMessage("Salary amount must be greater than zero.");

            RuleFor(s => s.Advance)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Advance amount must be greater than or equal to zero.");
        }
    }
}
