using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LegendsTeamVN.Core.Infrastructure.DependencyInjection.Extensions;

public static class SerilogExtensions
{
    public static IHostApplicationBuilder AddCoreSerilog(this IHostApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();

        builder.Services.AddSerilog((services, loggerConfiguration) => 
            loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
        );

        return builder;
    }
}
