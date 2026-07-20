using LegendsTeamVN.BadmintonClub.Domain.Repositories;
using LegendsTeamVN.BadmintonClub.Persistence.Repositories;
using LegendsTeamVN.Core.Persistence.DependencyInjection.Extensions;
using LegendsTeamVN.Core.Utilities.Options;
using LegendsTeamVN.Core.Application.Data;
using LegendsTeamVN.BadmintonClub.Persistence.Seeders;

using Microsoft.Extensions.DependencyInjection;

namespace LegendsTeamVN.BadmintonClub.Persistence.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, ConnectionStringsOptions connectionStrings)
    {
        services.AddInterceptorPersistence();

        services.AddDbContextUnitOfWork<BadmintonDbContext>(connectionStrings);

        services.AddScoped<ICourtRepository, CourtRepository>();

        return services;
    }

    public static IServiceCollection AddDataSeederBadminton(this IServiceCollection services)
    {
        services.AddTransient<IDataSeeder, CourtDataSeeder>();
        return services;
    }
}
