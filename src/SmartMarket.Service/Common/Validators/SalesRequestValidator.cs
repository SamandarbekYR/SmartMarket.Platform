using FluentValidation;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.SalesRequest;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMarket.Service.Common.Validators
{
    public class SalesRequestValidator : AbstractValidator<AddSalesRequestDto>
    {
        public SalesRequestValidator()
        {
            RuleFor(sr => sr.WorkerId)
                .NotEmpty()
                .WithMessage("Worker ID is required.");

            RuleFor(sr => sr.PayDeskId)
                .NotEmpty()
                .WithMessage("Pay Desk ID is required.");

            RuleFor(sr => sr.TotalCost)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total cost must be greater than or equal to zero.");

            RuleFor(sr => sr.CashSum)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Cash sum must be greater than or equal to zero.");

            RuleFor(sr => sr.CardSum)
                .NotEmpty()
                .WithMessage("Card sum must not be empty.");

            RuleFor(sr => sr.DebtSum)
                .NotNull()
                .GreaterThanOrEqualTo(0)
                .WithMessage("Debt sum must be greater than or equal to zero.");

            RuleForEach(sr => sr.ProductSaleItems)
                .SetValidator(new ProductSaleValidator());
        }
    }

}
