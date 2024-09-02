using FluentValidation;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgent;
using SmartMarket.Service.DTOs.PartnersCompany.ContrAgentPayment;
using SmartMarket.Service.DTOs.PartnersCompany.PartnerCompany;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Products.Debtors;
using SmartMarket.Service.DTOs.Products.DebtPayment;
using SmartMarket.Service.DTOs.Products.InvalidProduct;
using SmartMarket.Service.DTOs.Products.LoadReport;
using SmartMarket.Service.DTOs.Products.Product;
using SmartMarket.Service.DTOs.Products.ProductSale;
using SmartMarket.Service.DTOs.Products.ReplaceProduct;
using SmartMarket.Service.DTOs.Transaction;
using SmartMarket.Service.DTOs.Workers.Position;
using SmartMarket.Service.DTOs.Workers.Salary;
using SmartMarket.Service.DTOs.Workers.SalaryCheck;
using SmartMarket.Service.DTOs.Workers.SalaryWorker;
using SmartMarket.Service.DTOs.Workers.Worker;
using SmartMarket.Service.DTOs.Workers.WorkerDebt;
using SmartMarket.Service.DTOs.Workers.WorkerRole;

namespace SmartMarket.WebApi.Configurations;

public static class ValidatorsConfiguration
{
    public static void ConfigurationValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
        builder.Services.AddScoped<IValidator<AddPositionDto>, PositionValidator>();
        builder.Services.AddScoped<IValidator<AddSalaryDto>, SalaryValidator>();
        builder.Services.AddScoped<IValidator<AddSalaryCheckDto>, SalaryCheckValidator>();
        builder.Services.AddScoped<IValidator<AddCustomerDto>, CustomerValidator>();
        builder.Services.AddScoped<IValidator<AddExpenceDto>, ExpenceValidator>();
        builder.Services.AddScoped<IValidator<AddOrderDto>, OrderValidator>();
        builder.Services.AddScoped<IValidator<AddPartnerDto>, PartnerValidator>();
        builder.Services.AddScoped<IValidator<AddPayDesksDto>, PayDeskValidator>();
        builder.Services.AddScoped<IValidator<AddDebtPaymentDto>, DebtPaymentValidator>();
        builder.Services.AddScoped<IValidator<AddDebtorsDto>, DebtorValidator>();
        builder.Services.AddScoped<IValidator<AddInvalidProductDto>, InvalidProductValidator>();
        builder.Services.AddScoped<IValidator<AddLoadReportDto>, LoadReportValidator>();
        builder.Services.AddScoped<IValidator<AddProductDto>, ProductValidator>();
        builder.Services.AddScoped<IValidator<AddProductSaleDto>, ProductSaleValidator>();
        builder.Services.AddScoped<IValidator<AddReplaceProductDto>, ReplaceProductValidator>();
        builder.Services.AddScoped<IValidator<AddTransactionDto>, TransactionValidator>();
        builder.Services.AddScoped<IValidator<AddSalaryWorkerDto>, SalaryWorkerValidator>();
        builder.Services.AddScoped<IValidator<AddWorkerDebtDto>, WorkerDebtValidator>();
        builder.Services.AddScoped<IValidator<AddWorkerRoleDto>, WorkerRoleValidator>();
        builder.Services.AddScoped<IValidator<AddWorkerDto>, WorkerValidator>();
        builder.Services.AddScoped<IValidator<AddContrAgentDto>, ContrAgentValidator>();
        builder.Services.AddScoped<IValidator<AddContrAgentPaymentDto>, ContrAgentPaymentValidator>();
        builder.Services.AddScoped<IValidator<AddPartnerCompanyDto>, PartnerCompanyValidator>();
    }
}
