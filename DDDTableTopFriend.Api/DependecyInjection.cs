namespace DDDTableTopFriend.Api;

public static class DependecyInjection
{
    public static IServiceCollection AddPresenter(
        this IServiceCollection services)
    {
        services.AddControllers();
        return services;
    }
}
