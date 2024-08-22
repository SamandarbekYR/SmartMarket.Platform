using FluentValidation;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.DataAccess.Interfaces.Workers;
using SmartMarket.DataAccess.Repositories;
using SmartMarket.Service.Common.Mapper;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Customer;
using SmartMarket.Service.DTOs.Debtors;
using SmartMarket.Service.DTOs.DebtPayment;
using SmartMarket.Service.DTOs.Expence;
using SmartMarket.Service.DTOs.InvalidProduct;
using SmartMarket.Service.DTOs.LoadReport;
using SmartMarket.Service.DTOs.Order;
using SmartMarket.Service.DTOs.Partner;
using SmartMarket.Service.DTOs.PayDesks;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.DTOs.Salary;
using SmartMarket.Service.DTOs.SalaryCheck;
using SmartMarket.Service.Interfaces.Category;
using SmartMarket.Service.Interfaces.Customer;
using SmartMarket.Service.Interfaces.Debtor;
using SmartMarket.Service.Interfaces.DebtPayment;
using SmartMarket.Service.Interfaces.Expence;
using SmartMarket.Service.Interfaces.InvalidProduct;
using SmartMarket.Service.Interfaces.LoadReport;
using SmartMarket.Service.Interfaces.Order;
using SmartMarket.Service.Interfaces.Partner;
using SmartMarket.Service.Interfaces.PayDesks;
using SmartMarket.Service.Interfaces.Positions;
using SmartMarket.Service.Interfaces.Salary;
using SmartMarket.Service.Interfaces.SalaryCheck;
using SmartMarket.Service.Services.Category;
using SmartMarket.Service.Services.Customer;
using SmartMarket.Service.Services.Debtors;
using SmartMarket.Service.Services.DebtPayment;
using SmartMarket.Service.Services.Expence;
using SmartMarket.Service.Services.InvalidProduct;
using SmartMarket.Service.Services.LoadReport;
using SmartMarket.Service.Services.Order;
using SmartMarket.Service.Services.Partner;
using SmartMarket.Service.Services.PayDesks;
using SmartMarket.Service.Services.Positions;
using SmartMarket.Service.Services.Salary;
using SmartMarket.Service.Services.SalaryCheck;
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


/*builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("LocalDB"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});*/

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<ISalaryService, SalaryService>();
builder.Services.AddScoped<ISalaryCheckService, SalaryCheckService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IExpenceService, ExpenceService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPartnerService, PartnerService>();
builder.Services.AddScoped<IPayDeskService, PayDeskService>();
builder.Services.AddScoped<IDebtPaymentService, DebtPaymentService>();
builder.Services.AddScoped<IDebtorsService, DebtorsService>();
builder.Services.AddScoped<IInvalidProductService, InvalidProductService>();
builder.Services.AddScoped<ILoadReportService, LoadReportService>();

builder.Services.AddAutoMapper(typeof(MapperProfile));

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
