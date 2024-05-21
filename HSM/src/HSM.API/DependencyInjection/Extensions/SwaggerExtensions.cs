using HSM.API.DependencyInjection.Options;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace HSM.API.DependencyInjection.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerAPI(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Jwt bear",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name ="Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        }

        public static void UseSwaggerAPI(this WebApplication app)
        {
            app.UseSwagger(
                );
            app.UseSwaggerUI(options =>
            {
                foreach (var version in app.DescribeApiVersions().Select(version => version.GroupName))
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);

                options.DisplayRequestDuration();
                options.EnableTryItOutByDefault();
                options.DocExpansion(DocExpansion.None);
            });

            app.MapGet("/", () => Results.Redirect("/swagger/index.html"))
                .WithTags(string.Empty);
        }
    }
}
