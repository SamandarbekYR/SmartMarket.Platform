using FluentValidation;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;

namespace SmartMarket.Service.Common.Validators;

public class ReplaceProductValidator : AbstractValidator<AddReplaceProductDto>
{
    public ReplaceProductValidator()
    {
        RuleFor(rp => rp.ProductSaleId)
            .NotEmpty()
            .WithMessage("Product Sale ID is required.");

        RuleFor(rp => rp.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(rp => rp.Reason)
            .NotEmpty()
            .WithMessage("Reason for replacement is required.");
    }
}
