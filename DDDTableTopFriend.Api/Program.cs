using DDDTableTopFriend.Api;
using DDDTableTopFriend.Application;
using DDDTableTopFriend.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresenter()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Host
        .AddLogging();
}

var app = builder.Build();
{
    app.Services.MigrateDatabase();
    app.UseHttpsRedirection();
    app.AddLogging();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
