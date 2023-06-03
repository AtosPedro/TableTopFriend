using TableTopFriend.Api;
using TableTopFriend.Application;
using TableTopFriend.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
{
    _ = builder.Services
        .AddPresenter()
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddSwaggerGen(option =>
        {
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

    builder.Host
        .AddLogging();

    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy(
            name: "DevCORS",
            policy => policy.WithOrigins("http://localhost:4200")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials());
    });

    builder.Services.AddHealthChecks()
        .AddSqlServer(config["DBConfiguration:ConnectionString"]!)
        .AddRedis(config["CachingSettings:ConnectionString"]!);
}

var app = builder.Build();
{
    app.Services.MigrateDatabase();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Table Top Friend API V1"));
    }

    app.UseHttpsRedirection();

    app.UseCors("DevCORS");

    app.MapHealthChecks("/_health", new HealthCheckOptions
    {
        ResponseWriter = HealthChecks.UI.Client.UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.AddLogging();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
