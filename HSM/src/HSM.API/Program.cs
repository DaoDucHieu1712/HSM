using Carter;
using HSM.API.DependencyInjection.Extensions;
using HSM.API.Middleware;
using HSM.Application.DependencyInjection.Extensions;
using HSM.Persistance.DependencyInjection.Extensions;
using HSM.Persistance.DependencyInjection.Options;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using HSM.Persistance.Configurations;
using Serilog;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
//builder.Services
//              .AddAuthentication(options =>
//              {
//                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//              })
//              .AddJwtBearer(options =>
//              {
//                  var authSettings = builder.Configuration.GetSection("AzureAd").Get<AzureAdOptions>();

//                  options.Audience = authSettings.ClientId;
//                  options.Authority = authSettings.Authority;
//              });


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

builder.Services.AddAuthorization();
//builder.Services.AddSwaggerGen(option =>
//{
//    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
//    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter a valid token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });
//    option.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type=ReferenceType.SecurityScheme,
//                    Id="Bearer"
//                }
//            },
//            new string[]{}
//        }
//    });
//});



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();

// After map carter

app.MapCarter();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment() || builder.Environment.IsStaging())
{
    app.UseSwaggerAPI();
}
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
