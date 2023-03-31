using DDDTableTopFriend.Application.Common.Interfaces.Services;
using DDDTableTopFriend.Infrastructure.Authentication;
using DDDTableTopFriend.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTableTopFriend.Infrastructure;

public static class DepencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}
