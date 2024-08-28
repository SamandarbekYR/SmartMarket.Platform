using FluentValidation;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;

namespace SmartMarket.Service.Common.Validators;

public class PartnerCompanyValidator : AbstractValidator<AddPartnerCompanyDto>
{
    public PartnerCompanyValidator()
    {
        RuleFor(pc => pc.Name)
            .NotEmpty()
            .WithMessage("Partner company name is required.");

        RuleFor(pc => pc.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.");
    }
}
