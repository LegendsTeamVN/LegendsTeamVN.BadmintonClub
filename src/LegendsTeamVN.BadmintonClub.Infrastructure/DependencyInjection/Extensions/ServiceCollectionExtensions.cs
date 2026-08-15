using LegendsTeamVN.Core.Infrastructure.DependencyInjection.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LegendsTeamVN.BadmintonClub.Infrastructure.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCoreInfrastructure(configuration);

        services.AddMasstransitRabbitMQInfrastructure(
            configuration,
            AssemblyReference.Assembly);

        return services;
    }
}
