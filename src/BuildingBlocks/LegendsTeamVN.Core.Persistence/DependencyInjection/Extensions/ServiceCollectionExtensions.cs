using LegendsTeamVN.Core.Domain.Repositories;
using LegendsTeamVN.Core.Utilities.Options;
using LegendsTeamVN.Core.Persistence.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace LegendsTeamVN.Core.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDbContextUnitOfWork<TContext>(this IServiceCollection services, ConnectionStringsOptions configureOptions) where TContext : DbContext, IUnitOfWork
    {
        services.AddDbContext<TContext>((sp, options) =>
        {
            var updateAuditableEntitiesInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
            var softDeleteEntitiesInterceptor = sp.GetRequiredService<SoftDeleteInterceptor>();

            options.UseSqlServer(configureOptions.Database, sqlOptions =>
            {
                if (!string.IsNullOrEmpty(configureOptions.MigrationsAssembly))
                {
                    sqlOptions.MigrationsAssembly(configureOptions.MigrationsAssembly);
                }
                if (configureOptions.CommandTimeout.HasValue)
                {
                    sqlOptions.CommandTimeout(configureOptions.CommandTimeout.Value);
                }
            });

            options.AddInterceptors(updateAuditableEntitiesInterceptor, softDeleteEntitiesInterceptor);

            options.EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseLazyLoadingProxies();
        });

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<TContext>());
        return services;
    }

    public static IServiceCollection AddInterceptorPersistence(this IServiceCollection services)
    {
        services.AddScoped<AuditableEntityInterceptor>();
        services.AddScoped<SoftDeleteInterceptor>();

        return services;
    }
}
