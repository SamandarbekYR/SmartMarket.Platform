using FluentValidation;
using SmartMarket.Service.DTOs.Products.ProductSale;

namespace SmartMarket.Service.Common.Validators;

public class ProductSaleValidator : AbstractValidator<AddProductSaleDto>
{
    public ProductSaleValidator()
    {
        RuleFor(ps => ps.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required.");

        RuleFor(ps => ps.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product count must be greater than or equal to zero.");

        RuleFor(ps => ps.Discount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount must be greater than or equal to zero.");

        RuleFor(ps => ps.ItemTotalCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total cost must be greater than or equal to zero.");
    }
}