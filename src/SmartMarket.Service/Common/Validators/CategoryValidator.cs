using FluentValidation;
using SmartMarket.Service.DTOs.Category;

namespace SmartMarket.Service.Common.Validators;

public class CategoryValidator : AbstractValidator<CategoryDto>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Category name is required.");

        RuleFor(c => c.Description)
            .NotEmpty()
            .WithMessage("Category description is required.");
    }
}
