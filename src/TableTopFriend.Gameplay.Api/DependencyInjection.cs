﻿using TableTopFriend.Gameplay.Api.Hubs;

namespace TableTopFriend.Gameplay.Api;

public static class DependencyInjection
{
    public static WebApplication AddHubs(this WebApplication app)
    {
        app.MapHub<SessionHub>("/session-hub");
        app.MapHub<ChatHub>("/chat-hub");
        return app;
    }
}
