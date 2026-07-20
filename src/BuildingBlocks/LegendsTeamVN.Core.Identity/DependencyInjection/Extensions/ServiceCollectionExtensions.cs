using System.Text;

using LegendsTeamVN.Core.Application.Data;
using LegendsTeamVN.Core.Identity.Abstractions;
using LegendsTeamVN.Core.Identity.Attributes;
using LegendsTeamVN.Core.Identity.Data;
using LegendsTeamVN.Core.Identity.DependencyInjection.Options;
using LegendsTeamVN.Core.Identity.Entities;
using LegendsTeamVN.Core.Identity.Services;
using LegendsTeamVN.Core.Utilities.Options;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LegendsTeamVN.Core.Identity.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreIdentity(this IServiceCollection services, ConnectionStringsOptions connectionStrings, JwtOptions jwtOptions)
    {
        services.AddDatabaseIdentity(connectionStrings);

        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<AppIdentityDbContext>()
        .AddDefaultTokenProviders();


        services.AddJwtAuthenticationAPI(jwtOptions);

        

        services.AddIdentityContext();
        services.AddIdentityServices();

        return services;
    }

    public static IServiceCollection AddDataSeederIdentity(this IServiceCollection services)
    {
        services.AddTransient<IDataSeeder, IdentityDataSeeder>();
        return services;
    }

    public static IServiceCollection AddDatabaseIdentity(this IServiceCollection services, ConnectionStringsOptions connectionStrings)
    {
        if (connectionStrings.Database != null)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(connectionStrings.Database, sqlOptions =>
                {
                    if (!string.IsNullOrEmpty(connectionStrings.MigrationsAssembly))
                    {
                        sqlOptions.MigrationsAssembly(connectionStrings.MigrationsAssembly);
                    }
                    if (connectionStrings.CommandTimeout.HasValue)
                    {
                        sqlOptions.CommandTimeout(connectionStrings.CommandTimeout.Value);
                    }
                });
                options.EnableDetailedErrors()
               .UseLazyLoadingProxies();

            });
        }

        return services;
    }

    public static IServiceCollection AddJwtAuthenticationAPI(this IServiceCollection services, JwtOptions jwtOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            if (jwtOptions == null) return;

            o.SaveToken = true;

            var Key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Key),
                ClockSkew = TimeSpan.Zero
            };

            o.EventsType = typeof(SingleSessionJwtBearerEvents);

        });

        services.AddScoped<SingleSessionJwtBearerEvents>();

        services.AddAuthorization();
        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IUserManagerService, UserManagerService>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();

        return services;
    }

    public static IServiceCollection AddIdentityContext(this IServiceCollection services)
    {

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        return services;
    }
}
