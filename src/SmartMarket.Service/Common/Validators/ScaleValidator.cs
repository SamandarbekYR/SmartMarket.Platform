using FluentValidation;

using SmartMarket.Domain.Entities.Scales;
using SmartMarket.Service.DTOs.Scales;
using SmartMarket.Service.DTOs.Workers.Position;

namespace SmartMarket.Service.Common.Validators
{
    public class ScaleValidator : AbstractValidator<AddScaleDto>
    {
        public ScaleValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .MaximumLength(100)
                .WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.UpdateTimeInterval)
                .GreaterThan(0)
                .WithMessage("Update time interval must be greater than 0.");

            RuleFor(x => x.SelectFilePath)
                .NotEmpty()
                .WithMessage("File path is required.")
                .Must(path => System.IO.File.Exists(path))
                .WithMessage("File path must point to an existing file.");

            RuleFor(x => x.TXTFileName)
                .NotEmpty()
                .WithMessage("TXT file name is required.")
                .Must(fileName => fileName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase))
                .WithMessage("File name must have a .txt extension.");
        }
    }
}
