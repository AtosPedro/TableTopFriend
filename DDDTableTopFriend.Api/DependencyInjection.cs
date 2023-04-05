using System.Reflection;
using Mapster;

namespace DDDTableTopFriend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresenter(
        this IServiceCollection services)
    {
        services.AddControllers();
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        return services;
    }
}
