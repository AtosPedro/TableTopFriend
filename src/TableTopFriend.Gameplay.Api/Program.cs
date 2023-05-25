using TableTopFriend.Gameplay.Api;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddSignalR();
}

var app = builder.Build();
{
    app.AddHubs();
    app.UseAuthorization();
    app.Run();
}
