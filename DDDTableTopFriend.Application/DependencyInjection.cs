using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Mapster;

namespace DDDTableTopFriend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }
}
