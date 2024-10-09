using Microsoft.AspNetCore.SignalR;
using SmartMarket.Service.Common.Mapper;
using SmartMarket.Service.Helpers;
using SmartMarket.Service.Hubs;
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
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.ConfigurationValidators();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddHostedService<PostgresListenerService>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var hubContext = provider.GetRequiredService<IHubContext<ShipmentsHub>>();
    return new PostgresListenerService(configuration.GetConnectionString("DefaultConnection")!, hubContext);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Services.GetService<IHttpContextAccessor>() is not null)
{
    HttpContextHelper.Accessor = app.Services.GetRequiredService<IHttpContextAccessor>();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseMiddleware<CrosOriginAccessMiddleware>();
app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthorization();

app.MapControllers();
app.MapHub<ShipmentsHub>("/ShipMentsHub");
app.Run();
