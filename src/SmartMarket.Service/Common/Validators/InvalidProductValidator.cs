using FluentValidation;
using SmartMarket.Service.DTOs.Products.InvalidProduct;

namespace SmartMarket.Service.Common.Validators;

public class InvalidProductValidator : AbstractValidator<AddInvalidProductDto>
{
    public InvalidProductValidator()
    {
        RuleFor(ip => ip.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(ip => ip.ProductSaleId)
            .NotEmpty()
            .WithMessage("Product Sale ID is required.");

        RuleFor(ip => ip.ReturnReason)
            .NotEmpty()
            .WithMessage("Return reason is required.");
    }
}
