using System.Reflection;
using FluentValidation;
using LegendsTeamVN.Core.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace LegendsTeamVN.Core.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreApplication(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddMessageHandler(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddCoreBehaviors();
        });
        services.AddValidatorsFromAssembly(assembly);
        return services;
    }

    public static MediatRServiceConfiguration AddCoreBehaviors(this MediatRServiceConfiguration config)
    {
        config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        config.AddOpenBehavior(typeof(QueryCachingBehavior<,>));
        
        return config;
    }
}
