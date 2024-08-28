using FluentValidation;
using SmartMarket.Service.DTOs.Workers.Position;

namespace SmartMarket.Service.Common.Validators;

public class PositionValidator : AbstractValidator<AddPositionDto>
{
    public PositionValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Position name is required.");
    }
}