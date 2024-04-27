using FluentValidation;
using HSM.Application.Behaviors;
using HSM.Application.Mapper;
using HSM.Contract.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HSM.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
            => services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformancePipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TracingPipelineBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationDefaultBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionPipelineBehavior<,>))
            .AddValidatorsFromAssembly(HSM.Contract.AssemblyReference.Assembly, includeInternalTypes: true)
            .AddScoped<IPaginationService, PaginationService>();

        public static IServiceCollection AddConfigureAutoMapper(this IServiceCollection services)
            => services.AddAutoMapper(typeof(ServiceProfile));
    }
}
