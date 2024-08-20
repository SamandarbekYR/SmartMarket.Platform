using FluentValidation;
using SmartMarket.DataAccess.Interfaces;
using SmartMarket.DataAccess.Repositories;
using SmartMarket.Service.Common.Mapper;
using SmartMarket.Service.Common.Validators;
using SmartMarket.Service.DTOs.Category;
using SmartMarket.Service.DTOs.Position;
using SmartMarket.Service.Interfaces.Category;
using SmartMarket.Service.Interfaces.Positions;
using SmartMarket.Service.Services.Category;
using SmartMarket.Service.Services.Positions;
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


builder.Services.AddAutoMapper(typeof(MapperProfile));

builder.Services.AddScoped<IValidator<CategoryDto>, CategoryValidator>();
builder.Services.AddScoped<IValidator<AddPositionDto>, PositionValidator>();

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
