using FluentValidation;
using SmartMarket.Service.DTOs.Products.Product;

namespace SmartMarket.Service.Common.Validators;

public class ProductValidator : AbstractValidator<AddProductDto>
{
    public ProductValidator()
    {
        RuleFor(p => p.CategoryId)
            .NotEmpty()
            .WithMessage("Category ID is required.");

        RuleFor(p => p.ContrAgentId)
            .NotEmpty()
            .WithMessage("Counteragent ID is required.");

        RuleFor(p => p.WorkerId)
            .NotEmpty()
            .WithMessage("Worker ID is required.");

        RuleFor(p => p.Barcode)
            .NotEmpty()
            .WithMessage("Barcode is required.");

        RuleFor(p => p.PCode)
            .NotEmpty()
            .WithMessage("Product Code is required.");

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(p => p.Count)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product count must be greater than or equal to zero.");

        RuleFor(p => p.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal to zero.");

        RuleFor(p => p.SellPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Selling price must be greater than or equal to zero.");

        RuleFor(p => p.SellPricePersentage)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Sell price percentage must be greater than or equal to zero.");

        RuleFor(p => p.UnitOfMeasure)
            .NotEmpty()
            .WithMessage("Unit of measure is required.");

        RuleFor(p => p.PaymentStatus)
            .NotEmpty()
            .WithMessage("Payment status is required.");

        RuleFor(p => p.NoteAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Note amount must be greater than or equal to zero.");
    }
}