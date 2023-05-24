using DDDTableTopFriend.Gameplay.Api.Hubs;

namespace DDDTableTopFriend.Gameplay.Api;

public static class DependencyInjection
{
    public static WebApplication AddHubs(this WebApplication app)
    {
        app.MapHub<SessionHub>("/sessionHub");
        return app;
    }
}
