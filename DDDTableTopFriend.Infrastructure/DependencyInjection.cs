﻿using System.Text;
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
using DDDTableTopFriend.Infrastructure.Persistence.Context;
using DDDTableTopFriend.Infrastructure.Services.Mail;

namespace DDDTableTopFriend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddPersistence(configuration);
        services.AddHasherService(configuration);
        services.AddMailService(configuration);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
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

    public static IServiceCollection AddMailService(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var mailServiceSettings = new MailServiceSettings();
        configuration.Bind(MailServiceSettings.SectionName, mailServiceSettings);
        services.AddSingleton(Options.Create(mailServiceSettings));

        services.AddSingleton<IMailService, MailService>();
        return services;
    }

    public static IServiceCollection AddHasherService(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var hasherSettings = new HasherSettings();
        configuration.Bind(HasherSettings.SectionName, hasherSettings);
        services.AddSingleton(Options.Create(hasherSettings));

        services.AddSingleton<IHasher, Hasher>();
        return services;
    }

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var applicationDbSettings = new ApplicationDbSettings();
        configuration.Bind(ApplicationDbSettings.SectionName, applicationDbSettings);
        services.AddSingleton(Options.Create(applicationDbSettings));

        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddSingleton<ICampaignRepository, CampaignRepository>();
        return services;
    }
}
