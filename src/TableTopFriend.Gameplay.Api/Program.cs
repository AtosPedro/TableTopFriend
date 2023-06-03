using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using TableTopFriend.Gameplay.Api;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
{
    builder.Services.AddSignalR();
    builder.Services.AddHealthChecks()
        .AddSqlServer(config["DBConfiguration:ConnectionString"]!)
        .AddRedis(config["CachingSettings:ConnectionString"]!);
}

var app = builder.Build();
{
    app.AddHubs();
    app.UseAuthorization();
    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
    });
    app.Run();
}
