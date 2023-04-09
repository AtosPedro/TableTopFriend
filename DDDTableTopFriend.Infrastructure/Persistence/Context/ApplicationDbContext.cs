using DDDTableTopFriend.Domain.AggregateAudioEffect;
using DDDTableTopFriend.Domain.AggregateCampaign;
using DDDTableTopFriend.Domain.AggregateCharacter;
using DDDTableTopFriend.Domain.AggregateCharacter.Entities;
using DDDTableTopFriend.Domain.AggregateSession;
using DDDTableTopFriend.Domain.AggregateStatus;
using DDDTableTopFriend.Domain.AggregateUser;
using DDDTableTopFriend.Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DDDTableTopFriend.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<AudioEffect> AudioEffects { get; set; } = null!;
    public DbSet<Campaign> Campaigns { get; set; } = null!;
    public DbSet<Session> Sessions { get; set; } = null!;
    public DbSet<Character> Characters { get; set; } = null!;
    public DbSet<CharacterSheet> CharacterSheets { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;

    private readonly ApplicationDbSettings _applicationDbSettings;
    protected ApplicationDbContext(
        ApplicationDbSettings applicationDbSettings)
    {
        _applicationDbSettings = applicationDbSettings;
    }

    public new DbSet<T> Set<T>() where T : class => base.Set<T>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_applicationDbSettings.ConnectionString);
    }
}
