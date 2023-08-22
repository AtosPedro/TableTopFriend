using System.Text;
using TableTopFriend.Application.Common.Interfaces.Authentication;
using TableTopFriend.Application.Common.Interfaces.Persistence;
using TableTopFriend.Application.Common.Interfaces.Services;
using TableTopFriend.Infrastructure.Authentication;
using TableTopFriend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using TableTopFriend.Infrastructure.Persistence.Context;
using TableTopFriend.Infrastructure.Services.Mail;
using TableTopFriend.Infrastructure.Persistence.Interfaces;
using TableTopFriend.Infrastructure.Persistence.Repositories;
using System.Reflection;
using MediatR;
using StackExchange.Redis;
using TableTopFriend.Infrastructure.Services.Caching;
using Microsoft.EntityFrameworkCore;
using TableTopFriend.Infrastructure.Persistence.Interceptors;
using Quartz;
using TableTopFriend.Infrastructure.Jobs;
using Amazon.S3;
using TableTopFriend.Infrastructure.Services.Files;

namespace TableTopFriend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddAuth(configuration);
        services.AddPersistence(configuration);
        services.AddProcessOutboxMessagesJob();
        services.AddMailService(configuration);
        services.AddCaching(configuration);
        services.AddCloudStorage(configuration);
        services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly);
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

    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var applicationDbSettings = new ApplicationDbSettings();
        configuration.Bind(ApplicationDbSettings.SectionName, applicationDbSettings);
        services.AddSingleton(Options.Create(applicationDbSettings));

        services.AddSingleton<DomainEventsToOutboxMessagesInterceptor>();
        services.AddDbContext<ApplicationDbContext>();
        services.AddSingleton<IApplicationDbContext, ApplicationDbContext>();
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        services.AddSingleton<IAudioEffectRepository, AudioEffectRepository>();
        services.AddSingleton<ICampaignRepository, CampaignRepository>();
        services.AddSingleton<ICharacterRepository, CharacterRepository>();
        services.AddSingleton<ISessionRepository, SessionRepository>();
        services.AddSingleton<ISkillRepository, SkillRepository>();
        services.AddSingleton<IStatusRepository, StatusRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        return services;
    }

    public static IServiceCollection AddCloudStorage(
        this IServiceCollection services,
        ConfigurationManager configuration)

    {
        var fileStorageServiceSettings = new FileStorageServiceSettings();
        configuration.Bind(FileStorageServiceSettings.SectionName, fileStorageServiceSettings);
        services.AddSingleton(Options.Create(fileStorageServiceSettings));

        services.AddSingleton<IAmazonS3, AmazonS3Client>();
        services.AddSingleton<IFileStorageService, AmazonS3FileStorageService>();
        return services;
    }

    public static void MigrateDatabase(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<IApplicationDbContext>();
        if (context.Database.GetPendingMigrations().Any())
            context.Database.Migrate();
    }

    public static IServiceCollection AddCaching(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var cachingSettings = new CachingSettings();
        configuration.Bind(CachingSettings.SectionName, cachingSettings);
        services.AddSingleton(Options.Create(cachingSettings));

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(cachingSettings.ConnectionString));
        services.AddSingleton<ICachingService, RedisCachingService>();
        return services;
    }

    public static IServiceCollection AddProcessOutboxMessagesJob(this IServiceCollection services)
    {
        services.AddQuartz(configuration =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configuration
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(
                    trigger => 
                        trigger
                            .ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule
                                        .WithIntervalInSeconds(10)
                                        .RepeatForever()));

            configuration.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();
        return services;
    }
}
