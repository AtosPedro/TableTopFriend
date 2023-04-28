using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateSkill;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Domain.Common.Models;
using DDDTableTopFriend.Infrastructure.Persistence.Interceptors;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace DDDTableTopFriend.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Campaign> Campaigns { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<Skill> Skills { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<AudioEffect> AudioEffects { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    public override DatabaseFacade Database => base.Database;

    private readonly ApplicationDbSettings _applicationDbSettings;
    private readonly IServiceProvider _serviceProvider;

    public ApplicationDbContext(
        IOptions<ApplicationDbSettings>  applicationDbSettings,
        IServiceProvider serviceProvider)
    {
        _applicationDbSettings = applicationDbSettings.Value;
        _serviceProvider = serviceProvider;
    }

    public new DbSet<T> Set<T>() where T : class => base.Set<T>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var domainEventsToOutboxMessagesInterceptor = _serviceProvider.GetService<DomainEventsToOutboxMessagesInterceptor>()!;
        options.UseSqlServer(_applicationDbSettings.ConnectionString);
        options.AddInterceptors(domainEventsToOutboxMessagesInterceptor);
    }
}
