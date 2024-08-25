using FluentValidation;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.DataAccess.Repositories;
using SmartMarket.Service.Common.Mapper;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.ContrAgent;
using SmartMarket.Service.DTOs.ContrAgentPayment;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.DTOs.Debtors;
using SmartMarket.Service.DTOs.DebtPayment;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.InvalidProduct;
using SmartMarket.Service.DTOs.LoadReport;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PartnerCompany;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.DTOs.Product;
using SmartMarket.Service.DTOs.ProductSale;
using SmartMarket.Service.DTOs.ReplaceProduct;
using SmartMarket.Service.DTOs.Salary;
using SmartMarket.Service.DTOs.SalaryCheck;
using SmartMarket.Service.DTOs.SalaryWorker;
using SmartMarket.Service.DTOs.Transaction;
using SmartMarket.Service.DTOs.WorkerDebt;
using SmartMarket.Service.DTOs.WorkerRole;
using SmartMarket.Service.Interfaces.Category;
using SmartMarket.Service.Interfaces.ContrAgent;
using SmartMarket.Service.Interfaces.ContrAgentPayment;
using SmartMarket.Service.Interfaces.Customer;
using SmartMarket.Service.Interfaces.Debtor;
using SmartMarket.Service.Interfaces.DebtPayment;
using SmartMarket.Service.Interfaces.Expence;
using SmartMarket.Service.Interfaces.InvalidProduct;
using SmartMarket.Service.Interfaces.LoadReport;
using SmartMarket.Service.Interfaces.Order;
using SmartMarket.Service.Interfaces.Partner;
using SmartMarket.Service.Interfaces.PartnerCompany;
using SmartMarket.Service.Interfaces.PayDesks;
using SmartMarket.Service.Interfaces.Positions;
using SmartMarket.Service.Interfaces.Product;
using SmartMarket.Service.Interfaces.ProductSale;
using SmartMarket.Service.Interfaces.ReplaceProduct;
using SmartMarket.Service.Interfaces.Salary;
using SmartMarket.Service.Interfaces.SalaryCheck;
using SmartMarket.Service.Interfaces.SalaryWorker;
using SmartMarket.Service.Interfaces.Transaction;
using SmartMarket.Service.Interfaces.WorkerDebt;
using SmartMarket.Service.Interfaces.WorkerRole;
using SmartMarket.Service.Services.Category;
using SmartMarket.Service.Services.ContrAgent;
using SmartMarket.Service.Services.ContrAgentPayment;
using SmartMarket.Service.Services.Customer;
using SmartMarket.Service.Services.Debtors;
using SmartMarket.Service.Services.DebtPayment;
using SmartMarket.Service.Services.Expence;
using SmartMarket.Service.Services.InvalidProduct;
using SmartMarket.Service.Services.LoadReport;
using SmartMarket.Service.Services.Order;
using SmartMarket.Service.Services.Partner;
using SmartMarket.Service.Services.PartnerCompany;
using SmartMarket.Service.Services.PayDesks;
using SmartMarket.Service.Services.Positions;
using SmartMarket.Service.Services.Product;
using SmartMarket.Service.Services.ProductSale;
using SmartMarket.Service.Services.ReplaceProduct;
using SmartMarket.Service.Services.Salary;
using SmartMarket.Service.Services.SalaryCheck;
using SmartMarket.Service.Services.SalaryWorker;
using SmartMarket.Service.Services.Transaction;
using SmartMarket.Service.Services.WorkerDebt;
using SmartMarket.Service.Services.WorkerRole;
using SmartMarket.WebApi.Configurations;
using SmartMarket.WebApi.Extensions;
using SmartMarket.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomControllers();
builder.Services.AddCustomDbContext(builder.Configuration);
builder.ConfigureSwaggerAuth();
builder.ConfigureJwtAuth();
builder.ConfigureCORSPolicy();
builder.ConfigureServiceLayer();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.ConfigurationValidators();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseMiddleware<CrosOriginAccessMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();
