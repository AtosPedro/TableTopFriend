using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Mapster;
using TableTopFriend.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace TableTopFriend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly);
        return services;
    }

    public static IHostBuilder AddLogging(this IHostBuilder host)
    {
        host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));
        return host;
    }

    public static WebApplication AddLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}
