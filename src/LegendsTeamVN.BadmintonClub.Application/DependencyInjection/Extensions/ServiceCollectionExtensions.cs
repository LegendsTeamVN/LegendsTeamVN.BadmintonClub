using LegendsTeamVN.Core.Application.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LegendsTeamVN.BadmintonClub.Application.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMessageHandler(Assembly.GetExecutingAssembly());

        return services;
    }
}
