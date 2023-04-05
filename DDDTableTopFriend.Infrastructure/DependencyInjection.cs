using System.Text;
using DDDTableTopFriend.Application.Common.Interfaces.Authentication;
using DDDTableTopFriend.Application.Common.Interfaces.Persistence;
using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Infrastructure.Authentication;
using DDDTableTopFriend.Infrastructure.Persistence;
using DDDTableTopFriend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using DDDTableTopFriend.Infrastructure.Services.Security;

namespace DDDTableTopFriend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddOptions(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IHasher, Hasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<ICampaignRepository, CampaignRepository>();
        return services;
    }

    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();

        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(op => op.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });

        return services;
    }

    public static IServiceCollection AddOptions(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var hasherSettings = new HasherSettings();

        configuration.Bind(HasherSettings.SectionName, hasherSettings);
        services.AddSingleton(Options.Create(hasherSettings));
        return services;
    }
}
