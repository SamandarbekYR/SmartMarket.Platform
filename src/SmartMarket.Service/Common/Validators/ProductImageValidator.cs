using FluentValidation;
using SmartMarket.Service.DTOs.Products.ProductImage;

namespace SmartMarket.Service.Common.Validators;

public class ProductImageValidator : AbstractValidator<AddProductImageDto>
{
    public ProductImageValidator()
    {
        RuleFor(pi => pi.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(pi => pi.ImagePath)
            .NotEmpty()
            .WithMessage("Image path is required.");
    }
}
