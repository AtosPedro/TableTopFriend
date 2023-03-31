using DDDTableTopFriend.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace DDDTableTopFriend.Application;

public static class DepencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}
