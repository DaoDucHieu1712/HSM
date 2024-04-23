using Carter;
using HSM.API.DependencyInjection.Extensions;
using HSM.API.Middleware;
using HSM.Application.DependencyInjection.Extensions;
using HSM.Persistance.DependencyInjection.Extensions;
using HSM.Persistance.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddConfigureMediatR();
builder.Services.AddConfigureAutoMapper();

builder.Services.AddInterceptorDbContext();
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddMySqlConfiguration();
builder.Services.AddRepositoryBaseConfiguration();

//Add Middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddCarter();

builder.Services
    .AddSwaggerGenNewtonsoftSupport()
    .AddFluentValidationRulesToSwagger()
    .AddEndpointsApiExplorer()
    .AddSwaggerAPI();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });


    
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapCarter();

// After map carter
if (app.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.UseSwaggerAPI();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();


//app.MapControllers();

try
{
    await app.RunAsync();
    Log.Information("Stoped cleanly");
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    await app.StopAsync();
}
finally
{
    Log.CloseAndFlush();
    await app.DisposeAsync();
}

public partial class Program { }
