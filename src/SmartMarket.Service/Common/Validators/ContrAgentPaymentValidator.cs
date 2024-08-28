using FluentValidation;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;

namespace SmartMarket.Service.Common.Validators;

public class ContrAgentPaymentValidator : AbstractValidator<AddContrAgentPaymentDto>
{
    public ContrAgentPaymentValidator()
    {
        RuleFor(cap => cap.ContrAgentId)
            .NotEmpty()
            .WithMessage("Counteragent ID is required.");

        RuleFor(cap => cap.TotalDebt)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total debt amount must be greater than or equal to zero.");

        RuleFor(cap => cap.LastPayment)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Last payment amount must be greater than or equal to zero.");
    }
}
