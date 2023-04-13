using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Mapster;
using DDDTableTopFriend.Application.Common.Behaviors;
using FluentValidation;

namespace DDDTableTopFriend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(typeof(DependencyInjection).GetTypeInfo().Assembly);
        return services;
    }
}
