using FluentValidation;
using SmartMarket.Service.DTOs.LoadReport;

namespace SmartMarket.Service.Common.Validators;

public class LoadReportValidator : AbstractValidator<AddLoadReportDto>
{
    public LoadReportValidator()
    {
        RuleFor(lr => lr.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(lr => lr.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(lr => lr.ContrAgentId)
            .NotEmpty()
            .WithMessage("Counteragent ID is required.");

        RuleFor(lr => lr.TotalPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total price must be greater than or equal to zero.");
    }
}
