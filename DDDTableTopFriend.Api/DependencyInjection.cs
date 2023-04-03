namespace DDDTableTopFriend.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresenter(
        this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}
