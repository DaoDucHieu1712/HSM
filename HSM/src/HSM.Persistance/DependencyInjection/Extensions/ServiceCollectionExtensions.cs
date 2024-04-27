using HSM.Domain.Abstractions;
using HSM.Domain.Abstractions.Repositories;
using HSM.Persistance.DependencyInjection.Options;
using HSM.Persistance.Interceptors;
using HSM.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace HSM.Persistance.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMySqlConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>((provider, builder) =>
            {
                var outboxInterceptor = provider.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                var auditableInterceptor = provider.GetService<UpdateAuditableEntitiesInterceptor>();

                var configuration = provider.GetRequiredService<IConfiguration>();
                var options = provider.GetRequiredService<IOptionsMonitor<SqlServerRetryOptions>>();


                builder
           .EnableDetailedErrors(true)
           .EnableSensitiveDataLogging(true)
           .UseLazyLoadingProxies(true)
           .UseMySql(
                    connectionString: configuration.GetConnectionString("MySQL"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("MySQL")),
                    mySqlOptionsAction: optionsBuilder
                        => optionsBuilder.ExecutionStrategy(
                                dependencies => new MySqlRetryingExecutionStrategy(
                                    dependencies: dependencies,
                                    maxRetryCount: options.CurrentValue.MaxRetryCount,
                                    maxRetryDelay: options.CurrentValue.MaxRetryDelay,
                                    errorNumbersToAdd: options.CurrentValue.ErrorNumbersToAdd))
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name))
            .AddInterceptors(outboxInterceptor,
                    auditableInterceptor);
            });

            //services.AddScoped(typeof(ISpecificationEvaluator), typeof(SpecificationEvaluator));
        }

        public static void AddInterceptorDbContext(this IServiceCollection services)
        {
            services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
            services.AddSingleton<UpdateAuditableEntitiesInterceptor>();
        }

        public static void AddRepositoryBaseConfiguration(this IServiceCollection services)
        {
            services.AddTransient(typeof(IUnitOfWork), typeof(EFUnitOfWork));
            services.AddTransient(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));

            services.AddTransient(typeof(IUnitOfWorkDbContext<>), typeof(EFUnitOfWorkDbContext<>));
            services.AddTransient(typeof(IRepositoryBaseDbContext<,,>), typeof(RepositoryBaseDbContext<,,>));

        }

        public static OptionsBuilder<SqlServerRetryOptions> ConfigureSqlServerRetryOptions(this IServiceCollection services, IConfigurationSection section)
            => services
                .AddOptions<SqlServerRetryOptions>()
                .Bind(section)
                .ValidateDataAnnotations()
                .ValidateOnStart();
    }
}
