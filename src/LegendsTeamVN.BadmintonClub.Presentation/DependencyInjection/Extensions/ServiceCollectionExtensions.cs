using LegendsTeamVN.Core.Presentation.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LegendsTeamVN.BadmintonClub.Presentation.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        return services;
    }
}
